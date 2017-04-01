using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickup : MonoBehaviour {
    public int pointsToAdd;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerScript>() == null)
        {
            return; 
        }
        else
        {
            ScoreManager.addPoints(pointsToAdd);
            Destroy(gameObject);
        }
        
    }
}
