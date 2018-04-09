using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class whatever : MonoBehaviour {
	Vector3 offset;
	[SerializeField] Transform trans;
	// Use this for initialization
	void Start () {
		offset = trans.position - transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = trans.position - offset;
	}
}
