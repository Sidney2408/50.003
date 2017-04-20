using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class TouchControls : NetworkBehaviour
{
    int orient;
    private PlayerScript thePlayer;
    private GameObject player;

    // Use this for initialization
    void Start()
    {
        orient = 0;
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
            thePlayer = player.GetComponent<PlayerScript>();
            orient = (int)player.transform.localScale.x;
        }
        
    }

    // Update is called once per frame
    void Update()
    {

        if (player != null)
        {
            orient = player.GetComponent<PlayerScript>().xOrientation;
        }
   
    }


    public void LeftArrow()
    {
        Debug.Log("Left pressed");
        thePlayer.Move(-1);
    }
    public void RightArrow()
    {
        Debug.Log("Right pressed");
        thePlayer.Move(1);
    }
    public void UnpressedArrow()
    {
        Debug.Log("Unpressed");
        thePlayer.Move(0);
    }
    public void Jump()
    {
        Debug.Log("Jump pressed");
        thePlayer.Jump();
    }
    public void Shuriken()
    {
        Debug.Log("Fire Shuriken");
        thePlayer.fireStar(orient);
    }
    public void Stomp()
    {
        Debug.Log("Fire Shuriken");
        thePlayer.Stomp();
    }
}