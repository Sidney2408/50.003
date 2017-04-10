using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchControls : MonoBehaviour
{
    int orient;
    private PlayerScript thePlayer;

    // Use this for initialization
    void Start()
    {
        orient = 0;
    }

    // Update is called once per frame
    void Update()
    {
        thePlayer = FindObjectOfType<PlayerScript>();
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null) {
            orient = (int)player.transform.localScale.x;
        }
    }
    public void LeftArrow()
    {
        thePlayer.Move(-1);
    }
    public void RightArrow()
    {
        thePlayer.Move(1);
    }
    public void UnpressedArrow()
    {
        thePlayer.Move(0);
    }
    public void Jump()
    {
        Debug.Log("Jump button pressed");
        thePlayer.Jump();
    }
    public void Shuriken()
    {
        thePlayer.CmdFireStar(orient);
    }
}