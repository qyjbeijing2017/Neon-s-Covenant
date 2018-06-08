using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowPlane : MonoBehaviour {
    [SerializeField]Animation animation;

	// Use this for initialization
	void Start () {

        animation.CrossFade("Box006Up");
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
