using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audio_player : MonoBehaviour {
    bool played;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        GetComponent<AudioSource>().Stop();
        if (!played)
        {
            GetComponent<AudioSource>().Play();
            played = true;
        }

    }
}
