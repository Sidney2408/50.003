using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StompBehaviour : MonoBehaviour
{

    public float bounceVelocity;
    private Rigidbody2D playerRigidBody;
    // Use this for initialization
    void Start()
    {
        playerRigidBody = transform.parent.GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.tag =="Enemy")
        {

        }
            playerRigidBody.velocity = new Vector2(playerRigidBody.velocity.x, bounceVelocity);
        
       // playerRigidBody.velocity = new Vector2(playerRigidBody.velocity.x, bounceVelocity);

    }
}