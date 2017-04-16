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
        if (collision.tag.Equals("Enemy") )
        {
        }
        Instantiate(this.NinjaStarImpact, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
