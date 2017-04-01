using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour {
    public float moveSpeed;
    public bool moveRight;

    public Transform wallCheck;
    public float wallCheckRadius;
    public LayerMask whatIsWall;
    private bool isWalled;//We only want the object to decide if it is on the ground

    private bool noEdgeDetected;
    public Transform edgechecker;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        isWalled = Physics2D.OverlapCircle(wallCheck.position, wallCheckRadius, whatIsWall);
        noEdgeDetected = Physics2D.OverlapCircle(edgechecker.position, wallCheckRadius, whatIsWall);

        if (isWalled || !noEdgeDetected)
        {
            moveRight = !moveRight;
        }



        if (moveRight)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(moveSpeed, GetComponent<Rigidbody2D>().velocity.y);
        }
        else
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(-moveSpeed, GetComponent<Rigidbody2D>().velocity.y);
        }

        if (GetComponent<Rigidbody2D>().velocity.x > 0)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
        else if (GetComponent<Rigidbody2D>().velocity.x < 0)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);//set to -1 will flip player backwards

        }




    }
}
