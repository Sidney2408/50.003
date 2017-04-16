using UnityEngine;
using System.Collections;


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
        var health = other.GetComponent<Health>();
        if (health != null)
        {
            health.TakeDamage(100);
        }

        //Destroy(gameObject);

        //Always remember to set your collider to isTrigger!

    }//Player walks into the zone and trigger triggers


}
