using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyFinishedParticle : MonoBehaviour {
    private ParticleSystem thisParticleSystem;

	// Use this for initialization
	void Start () {
        this.thisParticleSystem = GetComponent<ParticleSystem>();
	}
	
	// Update is called once per frame
	void Update () {
        if (thisParticleSystem.isPlaying)
        {
            return;
        }
        Destroy(gameObject);
	}
    private void OnBecameInvisible()
    {
        Destroy(gameObject);//As soon as the particle goes offscreen, it is destroyed
    }
}
