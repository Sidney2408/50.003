using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;
using UnityEngine.SceneManagement;
using Prototype.NetworkLobby;
using UnityEngine.UI;





public class PlayerScript : NetworkBehaviour{
    //Code for moving 
    public float moveSpeed;
    public float jumpHeight;
    public Transform groundCheck;
    private float moveVelocity;

    //Code for jumping 
    public float groundCheckRadius;
    public LayerMask whatIsGround;
    private bool isGrounded;
    private bool exhausted;
    private int jump_count;
    public int max_jumps;

    //Stomping:
    public GameObject Stomper;
    public float stompDelay;
    public float stompCounter;

    //Animator
    private Animator anim;

    //Code for shooting 
    public GameObject ninjaStar;
    public GameObject gameCamera;
    public float shotDelay;
    private float shotDelayCounter;
    public int xOrientation;

    //Code for knockback 
    public float knockback;
    public float knockbackCount;
    public float knockbackLength;
    public bool knockFromRight;

    //Dead state
    [SyncVar(hook ="onDead")]
    public bool isDead;
    public GameObject GameOverScreen;
    public GameObject[] TouchControls;
    public GameObject Canvas;
    public Text name;
    public GameObject HealthBar;

    GameObject[] enemies;

    [SyncVar]
    public int num_dead;

    

    



    private void FixedUpdate()
    {

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);

        if (anim == null)
        {
            anim = GetComponent<Animator>();
        }
    }

    void Start() {
        if(enemies == null)
        {
            enemies = GameObject.FindGameObjectsWithTag("Enemy");
        }
        num_dead = 0;
        xOrientation = 1;
        isDead = false;
        GameOverScreen = GameObject.FindGameObjectWithTag("GameOverPanel");
        if (anim == null)
        {
            anim = GetComponent<Animator>();
        }

        if (gameCamera == null)
        {
            gameCamera = GameObject.FindGameObjectWithTag("MainCamera");
        }


        if (!isLocalPlayer)
        {
            //Canvas.SetActive(false);
            
            foreach (var button in TouchControls)
            {
                button.SetActive(false);
            }
           
        }


        jump_count = 0;

        
    }


    void Update()
    {
        HealthBar.transform.position = gameObject.transform.position+ new Vector3(0f, 0.75f,0f);
        if (!isLocalPlayer)
        {
            return;
        }
        if (num_dead>0&&!isDead)
        {
            Debug.Log("You win");
        }

        if (isDead)
        {
            this.GetComponent<ReskinAnimation>().spriteSheetName = "Tombstone";
            anim.SetFloat("Speed", 0f);
            gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, 0f);
            return;
        }

        
        if (shotDelayCounter>0)
        {
            shotDelayCounter -= Time.deltaTime;
        }
        if (stompCounter > 0)
        {
            stompDelay -= Time.deltaTime;
        }


        if (anim.GetBool("Sword"))
        {
            anim.SetBool("Sword", false);
            //anim.ResetTrigger("Attack");
        }

        

        //Orientation needs to be refined cos the camera was behind the player
        gameCamera.transform.position = new Vector3(transform.position.x, transform.position.y, -20);
        //exhausted = (jump_count == max_jumps);
        //if (isGrounded && exhausted)
        //{
        //    jump_count = 0;
        //    //Debug.Log("Exhausted");
        //}
        float y = GetComponent<Rigidbody2D>().velocity.y;
        float x = GetComponent<Rigidbody2D>().velocity.x;

        //gameObject.GetComponent<SpriteRenderer>().color = Color.red;



#if UNITY_STANDALONE || UNITY_WEBPLAYER


        moveVelocity = 0f;
        if (Input.GetKeyDown(KeyCode.Space))
        {

            Jump();

        }
       

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            Debug.Log("Moving Left");
            float y_component = GetComponent<Rigidbody2D>().velocity.y;
            //this.GetComponent<Rigidbody2D>().velocity = new Vector2(-moveSpeed, y_component);
            moveVelocity = -moveSpeed;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            Debug.Log("Moving Right");
            float y_component = GetComponent<Rigidbody2D>().velocity.y;
            //this.GetComponent<Rigidbody2D>().velocity = new Vector2(moveSpeed, y_component);
            moveVelocity = moveSpeed;
        }
       
        //Attacking code 
        if (Input.GetKeyDown(KeyCode.C))
        {

            //Debug.Log("Shots fired: " + firePoint.transform.position.x + "," + firePoint.transform.position.y);
            CmdFireStar(xOrientation);
            shotDelayCounter = shotDelay;
        }

        if (Input.GetKey(KeyCode.C))
        {
            shotDelayCounter -= Time.deltaTime;
            if (shotDelayCounter <= 0)
            {
                shotDelayCounter = shotDelay;
                CmdFireStar(xOrientation);

            }
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
          
            GetComponent<Rigidbody2D>().velocity = new Vector2(moveVelocity, -20);
        }
     
#endif

        //if (isGrounded && exhausted)
        //{
        //    jump_count = 0;
        //    Debug.Log("Exhausted");
        //}

        //KnockBack Code 
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


        //Animaor orientation code
        anim.SetFloat("Speed", Mathf.Abs(GetComponent<Rigidbody2D>().velocity.x));//Null ref exception called after attempting to establish a connection 

        if (x > 0)
        {
            //transform.localScale = new Vector3(1f, 1f, 1f);
            xOrientation = 1;
            //firePoint.localScale = new Vector3(1f, 1f, 1f);
        }
        else if (x < 0)
        {
            //transform.localScale = new Vector3(-1f, 1f, 1f);//set to -1 will flip player backwards
            xOrientation = -1;
            //firePoint.localScale = new Vector3(-1f, 1f, 1f);
        }
        if (isGrounded)
        {
            jump_count = 0;
            exhausted = false;
            //Debug.Log("Exhausted");
        }
        anim.SetBool("grounded", isGrounded);

    }

    //Input functions 
        public void Move(float moveInput)
    {
        moveVelocity = moveSpeed * moveInput;
    }

    [Command]
    public void CmdFireStar(int orient)
    {
        if (shotDelayCounter <= 0)
        {
            shotDelayCounter = shotDelay;
            Vector3 offset = new Vector3(1, 0, 0);
            int speed = 10;
            //var projectile = Instantiate(ninjaStar, firePoint.position, firePoint.rotation);//Correct
            if (orient < 0)
            {
                offset = new Vector3(-1, 0, 0);
            }
            var projectile = (GameObject)Instantiate(ninjaStar, gameObject.transform.position + offset, Quaternion.identity);
            speed = orient * speed;
            Debug.Log("Speed:" + speed + "," + transform.localScale.x);
            projectile.GetComponent<Rigidbody2D>().velocity = new Vector2(speed, 0);
            NetworkServer.Spawn(projectile);
            Destroy(projectile, 2.0f);
        }
    }

    public void Jump()
    {

        if (jump_count > max_jumps)
        {
            exhausted = true;
        }
        Debug.Log("Jump function called");
        Debug.Log("JUMPCOUNT IS " + jump_count);
        Debug.Log("Exhausted? " + exhausted);

        if (!exhausted)
        {
            if (isGrounded)
            {
                Debug.Log("Jump high " + jump_count);
                float x_component = GetComponent<Rigidbody2D>().velocity.x;
                this.GetComponent<Rigidbody2D>().velocity = new Vector2(x_component, jumpHeight);
                jump_count++;
                isGrounded = false;

            }
            else
            {
                Debug.Log("Jump low " + jump_count);
                float x_component = GetComponent<Rigidbody2D>().velocity.x;
                this.GetComponent<Rigidbody2D>().velocity = new Vector2(x_component, jumpHeight / 4 * 3);
                jump_count++;
                exhausted = true;
                Debug.Log("is exhausted now");
            }

        }
        //Debug.Log("Jump function called");
        //if(jump_count > max_jumps &&isGrounded)
        //{
        //    jump_count = 0;
        //}
        //if (jump_count<max_jumps)
        //{
        //    jump_count++;
        //    float x_component = GetComponent<Rigidbody2D>().velocity.x;
        //    this.GetComponent<Rigidbody2D>().velocity = new Vector2(x_component, jumpHeight);
        //}
    }

    public void Stomp()
    {
        if (stompCounter <= 0)
        {
            shotDelayCounter = stompDelay;
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, -25);
        }
    }



    void OnDestroy()//This will trigger for both games as long as a player object is destroyed 
    {
        /*
        if (isLocalPlayer)
        {
            gameCamera.transform.position = new Vector3(0, 0, -20);
            if (GameOverScreen != null)
            {
                GameOverScreen.GetComponent<GameOverPanel>().ToggleVisibility(true);
            }
        }
        
         MissingReferenceException: The object of type 'GameObject' has been destroyed but you are still trying to access it.
Your script should either check if it is null or you should not destroy the object.
PlayerScript.OnDestroy () (at Assets/Scripts/PlayerScript.cs:239)
         */
    }

    public void onDead(bool dead)
    {
        Debug.Log("You died? " + dead);
        num_dead++;
        Debug.Log(num_dead);
        if (dead) {
            if (isLocalPlayer)
            {
                gameCamera.transform.position = new Vector3(0, 0, -20);
                if (GameOverScreen != null)
                {
                    GameOverScreen.GetComponent<GameOverPanel>().ToggleVisibility(true);
                }
            }

        }
    }





}




