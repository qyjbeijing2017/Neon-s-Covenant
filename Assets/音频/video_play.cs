using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class video_play : MonoBehaviour {
    [SerializeField]MovieTexture movie;

	// Use this for initialization
	void Start () {
        GetComponent<RawImage>().texture = movie;
        movie.Play();

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
