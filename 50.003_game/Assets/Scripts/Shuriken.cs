using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Shuriken : MonoBehaviour {
    public float speed;

    public float rotationSpeed;
    GameObject player;
    public GameObject EnemyDeathEffect;
    public GameObject NinjaStarImpact;
    //public GameObject firepoint;


    // Use this for initialization
    void Start () {
        Debug.Log("Shuriken final speed " + speed);
        Debug.Log("Shuriken coordinates:" + transform.position.x + "," + transform.position.y);

        //player = FindObjectOfType<PlayerScript>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update () {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Edit > ProjectSettings > Physics2D settings (normal mode) > Collision Layer matrix
        Debug.Log("Hit something");
        var hit = collision.gameObject;
        var health = hit.GetComponent<Health>();
        var player = hit.GetComponent<PlayerScript>();
        if (health != null&& player!= null)
        {
            health.TakeDamage(10);
            player.knockbackCount = player.knockbackLength;
            if (collision.transform.position.x<transform.position.x)
            {
                player.knockFromRight = true;
            }
            else
            {
                player.knockFromRight = false;
            }

        }
        Destroy(gameObject);
    }
}
