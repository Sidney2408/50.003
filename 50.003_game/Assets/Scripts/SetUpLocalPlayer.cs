using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SetUpLocalPlayer : NetworkBehaviour {
    [SyncVar]//Tells server to sync to client
    public string pname = "player";
    [SyncVar]
    public Color playerColor = Color.white;//Not changing leh?

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
            //pname = GUI.TextField(new Rect(25, Screen.height - 40,100, 30), pname);
            gameObject.tag = "Player";
            gameObject.GetComponent<SpriteRenderer>().color = playerColor;

            Debug.Log("Color: " + playerColor);
        }

        //For players who came late
        if (playerColor.Equals(Color.green))
        {

        }
        else if (playerColor.Equals(Color.black)){

        }
        else if (playerColor.Equals(Color.yellow)){

        }
        else if (playerColor.Equals(Color.magenta)){

        }
    }
    

    private void Update()
    {
        
    }

    [Command]
    public void CmdChangeName()
    {

    }


}
