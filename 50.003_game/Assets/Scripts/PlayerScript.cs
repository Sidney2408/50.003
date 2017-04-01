using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Multilinecomments
//Select a bunch of text then Ctrl+K+C
//Bug fixes:
//1.GetKey, not GetKeyDown for moving (ConstantForce velocity)
//2.Don't put 0 as x argument for move L/R; it kills upward velocity instantlry
//float y_component = GetComponent<Rigidbody2D>().velocity.y; to capture initial velocity components

public class PlayerScript : MonoBehaviour {
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


    private void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);

        //Create a circle
        //TransformPoint, we need posn of Transform
        //
        anim = GetComponent<Animator>();
    }

    // Use this for initialization
    void Start() {
        jump_count = 2;
        if (gameCamera == null)
        {
            // UNDONE:  
            gameCamera = GameObject.FindGameObjectWithTag("MainCamera");
            gameCamera.GetComponent<CameraController>().enabled = true;
        }
    }


    //}

    // Update is called once per frame
    void Update()
    {
        exhausted = (jump_count == max_jumps);
        float y = GetComponent<Rigidbody2D>().velocity.y;
        float x = GetComponent<Rigidbody2D>().velocity.x;
        //

#if UNITY_STANDALONE || UNITY_WEBPLAYER
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


        moveVelocity = 0f;
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            float y_component = GetComponent<Rigidbody2D>().velocity.y;
            //this.GetComponent<Rigidbody2D>().velocity = new Vector2(-moveSpeed, y_component);
            moveVelocity = -moveSpeed;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            float y_component = GetComponent<Rigidbody2D>().velocity.y;
            //this.GetComponent<Rigidbody2D>().velocity = new Vector2(moveSpeed, y_component);
            moveVelocity = moveSpeed;
        }
       
        if (Input.GetKeyDown(KeyCode.C))
        {
            FireStar();
            shotDelayCounter = shotDelay;
        }
        if (Input.GetKey(KeyCode.C))
        {
            shotDelayCounter -= Time.deltaTime;
            if (shotDelayCounter <= 0)
            {
                shotDelayCounter = shotDelay;
                FireStar();

            }
        }

#endif
        if (isGrounded && exhausted)
        {
            jump_count = 0;
            Debug.Log("Exhausted");
        }
        this.GetComponent<Rigidbody2D>().velocity = new Vector2(moveVelocity, GetComponent<Rigidbody2D>().velocity.y);
        //This code will instantly stop the characther if no key is pressed



        anim.SetFloat("Speed", Mathf.Abs(GetComponent<Rigidbody2D>().velocity.x));
        if (x > 0)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
        else if (x < 0)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);//set to -1 will flip player backwards
        }
        anim.SetBool("grounded", isGrounded);
    }


        public void Move(float moveInput)
    {
        moveVelocity = moveSpeed * moveInput;
    }

    public void FireStar()
    {
        Instantiate(ninjaStar, firePoint.position, firePoint.rotation);
    }

    public void Jump()
    {
        Debug.Log("JumpCount: "+jump_count);
        Debug.Log("MaxJump: " + max_jumps);
        Debug.Log("Exhausted: " + exhausted);
        Debug.Log("Grounded: " + isGrounded);
        
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
}

    


