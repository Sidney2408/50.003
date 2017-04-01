using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shuriken : MonoBehaviour {
    public float speed;
    public float rotationSpeed;
    PlayerScript player;
    public GameObject EnemyDeathEffect;
    public GameObject NinjaStarImpact;


    // Use this for initialization
    void Start () {
        Destroy(gameObject, 1);
        player = FindObjectOfType<PlayerScript>();
        if (player.transform.localScale.x<0)
        {
            speed = -speed;
            rotationSpeed = -rotationSpeed;
        }
	}
	
	// Update is called once per frame
	void Update () {
        GetComponent<Rigidbody2D>().velocity = new Vector2(speed, GetComponent<Rigidbody2D>().velocity.y);
        GetComponent<Rigidbody2D>().angularVelocity = rotationSpeed;

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Enemy")
        {
            Instantiate(EnemyDeathEffect, collision.transform.position, collision.transform.rotation);
            Destroy(collision.gameObject);
        }
        Instantiate(NinjaStarImpact, transform.position, transform.rotation);

        Destroy(gameObject);
        //Edit > ProjectSettings > Physics2D settings (normal mode) > Collision Layer matrix
    }
}
