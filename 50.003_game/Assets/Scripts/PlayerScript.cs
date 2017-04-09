using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;

//Multilinecomments
//Select a bunch of text then Ctrl+K+C
//Bug fixes:
//1.GetKey, not GetKeyDown for moving (ConstantForce velocity)
//2.Don't put 0 as x argument for move L/R; it kills upward velocity instantlry
//float y_component = GetComponent<Rigidbody2D>().velocity.y; to capture initial velocity components
//Always set animator to animate physics!

public class PlayerScript : NetworkBehaviour{
    public float moveSpeed;
    public float jumpHeight;
    public Transform groundCheck;
    private float moveVelocity;

    public float groundCheckRadius;
    public LayerMask whatIsGround;
    private bool isGrounded;//We only want the object to decide if it is on the ground
    private bool exhausted;
    private int jump_count;

    public int max_jumps;
    private Animator anim;

    public Transform firePoint;
    public GameObject ninjaStar;
    public GameObject gameCamera;
    public float shotDelay;
    private float shotDelayCounter;

    public float knockback;
    public float knockbackCount;
    public float knockbackLength;
    public bool knockFromRight;



    private void FixedUpdate()
    {

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);

        //Create a circle
        //TransformPoint, we need posn of Transform
        //
        if (anim == null)
        {
            anim = GetComponent<Animator>();
        }
    }

    // Use this for initialization
    void Start() {

        


        if (anim == null)
        {
            anim = GetComponent<Animator>();
        }

        jump_count = 2;
        if (gameCamera == null)
        {
            // UNDONE:  
            gameCamera = GameObject.FindGameObjectWithTag("MainCamera");
            //gameCamera.GetComponent<CameraController>().enabled = true;
        }


        gameObject.GetComponent < NetworkAnimator > ().SetParameterAutoSend(0, true);
       


    }


    //}

    // Update is called once per frame
    void Update()
    {

        if (!isLocalPlayer)
        {
            return;
        }

        if (anim.GetBool("Sword"))
        {
            anim.SetBool("Sword", false);
            anim.ResetTrigger("Attack");
        }

        

        //Orientation needs to be refined cos the camera was behind the player
        gameCamera.transform.position = new Vector3(transform.position.x, transform.position.y, -20);


        exhausted = (jump_count == max_jumps);
        float y = GetComponent<Rigidbody2D>().velocity.y;
        float x = GetComponent<Rigidbody2D>().velocity.x;


#if UNITY_STANDALONE || UNITY_WEBPLAYER

        if (Input.GetKeyDown(KeyCode.S))
        {
            anim.SetBool("Sword", true);
            anim.SetTrigger("Attack");
            //gameObject.GetComponent<NetworkAnimator>().SetTrigger("Attack");
            if (NetworkServer.active)
            {
                //anim.ResetTrigger("Attack");
            }


        }
        moveVelocity = 0f;
        if (Input.GetKeyDown(KeyCode.Space) && !exhausted)
        {
  
            jump_count++;
            if (jump_count < 1)
            {
                float x_component = GetComponent<Rigidbody2D>().velocity.x;
                this.GetComponent<Rigidbody2D>().velocity = new Vector2(x_component, jumpHeight);
            }
            else
            {
                float x_component = GetComponent<Rigidbody2D>().velocity.x;
                this.GetComponent<Rigidbody2D>().velocity = new Vector2(x_component, jumpHeight / 4 * 3);
            }

        }
        if (isGrounded && exhausted)
        {
            jump_count = 0;
            //Debug.Log("Exhausted");
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            Debug.Log("Moving Left");
            float y_component = GetComponent<Rigidbody2D>().velocity.y;

            this.GetComponent<Rigidbody2D>().velocity = new Vector2(-moveSpeed, y_component);
           // Debug.Log("Fire coordinates: " + firePoint.transform.position.x + "," + firePoint.transform.position.y);
            moveVelocity = -moveSpeed;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            Debug.Log("Moving Right");
            float y_component = GetComponent<Rigidbody2D>().velocity.y;
           // Debug.Log("Fire coordinates: " + firePoint.transform.position.x + "," + firePoint.transform.position.y)
            this.GetComponent<Rigidbody2D>().velocity = new Vector2(moveSpeed, y_component);
            moveVelocity = moveSpeed;
        }
       
        if (Input.GetKeyDown(KeyCode.C))
        {

            Debug.Log("Shots fired: " + firePoint.transform.position.x + "," + firePoint.transform.position.y);
            CmdFireStar((int)transform.localScale.x);
            shotDelayCounter = shotDelay;
        }

        if (Input.GetKey(KeyCode.C))
        {
            shotDelayCounter -= Time.deltaTime;
            if (shotDelayCounter <= 0)
            {
                shotDelayCounter = shotDelay;
                CmdFireStar((int)transform.localScale.x);

            }
        }


       

#endif

        if (isGrounded && exhausted)
        {
            jump_count = 0;
            Debug.Log("Exhausted");
        }

        if (knockbackCount <= 0)
        {
            this.GetComponent<Rigidbody2D>().velocity = new Vector2(moveVelocity, GetComponent<Rigidbody2D>().velocity.y);
            //This code will instantly stop the characther if no key is pressed
        }
        else
        {
            //if not knockback
            if (knockFromRight)
            {
                this.GetComponent<Rigidbody2D>().velocity = new Vector2(-knockback, GetComponent<Rigidbody2D>().velocity.y);
            }
            if (!knockFromRight)
            {
                this.GetComponent<Rigidbody2D>().velocity = new Vector2(knockback, GetComponent<Rigidbody2D>().velocity.y);
            }
            knockbackCount -= Time.deltaTime;//Countdown
        }



        anim.SetFloat("Speed", Mathf.Abs(GetComponent<Rigidbody2D>().velocity.x));//Null ref exception called after attempting to establish a connection 

        if (x > 0)
        {
            //Debug.Log("Facing forwards");
            transform.localScale = new Vector3(1f, 1f, 1f);
            //firePoint.localScale = new Vector3(1f, 1f, 1f);
        }
        else if (x < 0)
        {
            //Debug.Log("Facing backwards");
            transform.localScale = new Vector3(-1f, 1f, 1f);//set to -1 will flip player backwards
            //firePoint.localScale = new Vector3(-1f, 1f, 1f);
        }
        anim.SetBool("grounded", isGrounded);
    }


        public void Move(float moveInput)
    {
        moveVelocity = moveSpeed * moveInput;
    }


    [Command]
    public void CmdFireStar(int orient)
    {
        Vector3 offset = new Vector3(1, 0, 0);
        int speed = 5;
        //var projectile = Instantiate(ninjaStar, firePoint.position, firePoint.rotation);//Correct
        if (orient <0)
        {
            offset = new Vector3(-1, 0, 0);
        }
        
        
        var projectile = (GameObject)Instantiate(ninjaStar, gameObject.transform.position+offset, firePoint.rotation);//cast to game object?

        speed = orient * speed;
        //Debug.Log("Instantiantion coordinates:" + point.transform.position.x + "," + point.transform.position.y);
        Debug.Log("Speed:" + speed + "," + transform.localScale.x);

        projectile.GetComponent<Rigidbody2D>().velocity = new Vector2(speed, GetComponent<Rigidbody2D>().velocity.y);
        NetworkServer.Spawn(projectile);//Needs revision 
        Destroy(projectile, 2.0f);
    }

    public void Jump()
    {
        if(jump_count > max_jumps &&isGrounded)
        {
            jump_count = 0;
        }
        if (jump_count<max_jumps)
        {
            jump_count++;
            float x_component = GetComponent<Rigidbody2D>().velocity.x;
            this.GetComponent<Rigidbody2D>().velocity = new Vector2(x_component, jumpHeight);
        }
    }

    void OnDestroy()
    {
        if (isLocalPlayer)
        {
            gameCamera.transform.position = new Vector3(0, 0, -20);
        }
        /*
         MissingReferenceException: The object of type 'GameObject' has been destroyed but you are still trying to access it.
Your script should either check if it is null or you should not destroy the object.
PlayerScript.OnDestroy () (at Assets/Scripts/PlayerScript.cs:239)
         */
    }

}




