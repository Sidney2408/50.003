using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchControls : MonoBehaviour {

    private PlayerScript thePlayer;

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        thePlayer = FindObjectOfType<PlayerScript>();

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
        thePlayer.Jump();
    }
    public void Shuriken()
    {
        thePlayer.FireStar();
    }
}
