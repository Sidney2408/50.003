using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillPlayer : MonoBehaviour {
    public LevelManager levelManager;

    // Use this for initialization
    void Start () {
        levelManager = FindObjectOfType<LevelManager>();// find objects with level manager attached to it
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnTriggerEnter2D(Collider2D other)//Whatever enters the colliderzone
    {

       //Always remember to set your collider to isTrigger!
        if (other.name.Equals( "Player"))
        {
            levelManager.RespawnPlayer();
        }
    }//Player walks into the zone and trigger triggers

}
