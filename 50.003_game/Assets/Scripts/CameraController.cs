using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
    public PlayerScript player;
    public bool isFollowing;
    public float xOffset;
    public float yOffset;


    // Use this for initialization
    void Start () {
        //player = FindObjectOfType<PlayerScript>();
        isFollowing = true;
		
	}
	
	// Update is called once per frame
	void Update () {
        if (isFollowing)
        {
           transform.position = new Vector3(player.transform.position.x+xOffset, player.transform.position.y+yOffset, transform.position.z);
            //There is no player. for the transform z position!
        }
		
	}
}
