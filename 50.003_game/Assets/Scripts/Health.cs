﻿using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using System.Collections;

public class Health : NetworkBehaviour{

    public const int maxHealth = 100;
    public bool destroyOnDeath;

    [SyncVar(hook = "OnChangeHealth")]
    public int currentHealth = maxHealth;//Sync var has indeed changed 

    public RectTransform healthBar;
    //private NetworkStartPosition[] spawnPoints;

    void Start()
    {
        /*
        if (isLocalPlayer)
        {
            spawnPoints = FindObjectsOfType<NetworkStartPosition>();
        }
        */
    }

    public void TakeDamage(int amount)
    {
        if (!isServer)
        {
            Debug.Log("NOT server");
            return;
        }
            

        currentHealth -= amount;//HP reduced
        Debug.Log("Current Health: " + currentHealth);
        Debug.Log("Your health is supposed to drop here");

        if (currentHealth <= 0)
        {
            if (destroyOnDeath)
            {
                Destroy(gameObject);

                if (isLocalPlayer)
                {
                    Debug.Log("You died!");
                   
                }
                
            }
        }
    }

    void OnChangeHealth(int newhealth)
    {
        /*
         When you use hook functions the syncvar isn't automatically updated, this is by design.
         */
        healthBar.sizeDelta = new Vector2(newhealth, healthBar.sizeDelta.y);
        currentHealth = newhealth;//Forgot to put this in!
        //Funnily enough this function procs
        Debug.Log("SyncVar Hook called");
    }


    /*
    [ClientRpc]
    void RpcRespawn()
    {
        if (isLocalPlayer)
        {
            // Set the spawn point to origin as a default value
            Vector3 spawnPoint = Vector3.zero;

            // If there is a spawn point array and the array is not empty, pick one at random
            if (spawnPoints != null && spawnPoints.Length > 0)
            {
                spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)].transform.position;
            }

            // Set the player’s position to the chosen spawn point
            transform.position = spawnPoint;
        }
    }
    */
}