using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SetUpLocalPlayer : NetworkBehaviour {
    [SyncVar]//Tells server to sync to client
    string pname = "player";
    private void OnGUI()
    {
        //pname = GUI.TextField(new Rect(25, Screen.height - 40, 100, 30), pname);
    }

    // Use this for initialization
    void Start () {
        if (!isLocalPlayer)
        {
            gameObject.tag = "Enemy";
        }
        else
        {
            gameObject.tag = "Player";
            //gameObject.GetComponent<PlayerScript>().enabled = true;
        }
    }

    private void Update()
    {
        if (isLocalPlayer)
        {
//set the name of the player
        }
    }

    [Command]
    public void CmdChangeName()
    {

    }


}
