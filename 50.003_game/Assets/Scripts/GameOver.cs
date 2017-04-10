using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour {

    public float timer = 3;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        timer -= Time.deltaTime;
        if (timer <0)
        {
            //Application.LoadLevel("NetworkLobby");
            SceneManager.LoadScene("NetworkLobby");
        }
	}
}
