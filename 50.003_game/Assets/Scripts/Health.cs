using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using System.Collections;

public class Health : NetworkBehaviour{

    public const int maxHealth = 125;
    public bool destroyOnDeath;
    public float invincibilityCounter;
    public const float invincibilityFrames = 0.75f;



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
    private void Update()
    {
        if (invincibilityCounter > 0)
        {
            invincibilityCounter -= Time.deltaTime;
        }
        else if (invincibilityCounter<0)
        {
            invincibilityCounter = 0;
        }
    }

    public void TakeDamage(int amount)
    {
        GetComponent<AudioSource>().Play();
        if (invincibilityCounter >0)
        {
            Debug.Log("Can't be damaged");
            return;
        }
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
                //Destroy(gameObject);
                gameObject.GetComponent<PlayerScript>().isDead = true;
                if (isLocalPlayer)
                {
                    Debug.Log("You died!");
                   
                }
                
            }
        }
        invincibilityCounter = invincibilityFrames;

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
}