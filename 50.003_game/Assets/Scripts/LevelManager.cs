using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {
    public GameObject checkPoint;
    public PlayerScript player;//Access the script we made for the player object
    public GameObject deathParticle;
    public GameObject respawnParticle;
    private CameraController camera;
    public int PointPenaltyOnDeath;
    private float GravityStore;


    // Use this for initialization
    void Start () {
        player = FindObjectOfType<PlayerScript>();
        camera = FindObjectOfType<CameraController>();
        GravityStore = 5f;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void RespawnPlayer()
    {
        Instantiate(deathParticle, player.transform.position, player.transform.rotation);
        player.enabled = false;
        camera.isFollowing = false;
        ScoreManager.addPoints(-PointPenaltyOnDeath);
        //player.GetComponent<Rigidbody2D>().gravityScale = 0f;
        //player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;//Stops the player from moving after he respawns
        //Note that after death, the player script is disabled so nothing is resetting the velocity to zero
        //player.GetComponent<Rigidbody2D>().gravityScale = GravityStore;

        //You need a posn and a new rotation when instantiating new object
        Debug.Log("Player respawned");
        player.transform.position = checkPoint.transform.position;//Reassigns the position of the player
        player.enabled = true;
        camera.isFollowing = true;
        Instantiate(respawnParticle, checkPoint.transform.position, checkPoint.transform.rotation);

    }
}
