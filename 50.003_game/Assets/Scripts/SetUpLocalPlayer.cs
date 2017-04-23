using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SetUpLocalPlayer : NetworkBehaviour {
    [SyncVar]//Tells server to sync to client
    public string playerName = "player";
    [SyncVar]
    public Color playerColor = Color.white;

    void Start () {

        if (!isLocalPlayer)
        {
            gameObject.tag = "Enemy";
            Debug.Log("Tagged an enemy");
        }
        else
        {
            gameObject.tag = "Player";
            Debug.Log("Player name:" +playerName);
            Debug.Log("Color: " + playerColor);
        }
        gameObject.GetComponent<PlayerScript>().name.text = playerName;
        if (playerColor.Equals(Color.green))
        {
            gameObject.GetComponent<ReskinAnimation>().spriteSheetName = "Sprites_Green";
            Debug.Log("Sprite palette: green");
        }
        else if (playerColor.Equals(Color.black)){
           gameObject.GetComponent<ReskinAnimation>().spriteSheetName = "Sprites_Black";
            Debug.Log("Sprite palette: black");
        }
        else if (playerColor.Equals(Color.yellow)){
            gameObject.GetComponent<ReskinAnimation>().spriteSheetName = "Sprites_Yellow";
            Debug.Log("Sprite palette: yellow");
        }
        else if (playerColor.Equals(Color.magenta)){
            gameObject.GetComponent<ReskinAnimation>().spriteSheetName = "Sprites_Magenta";
            Debug.Log("Sprite palette: magenta");
        }
        else {
            Debug.Log("Sprite palette: undefined");
        }
    }
}
