using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestEnd : MonoBehaviour {
    public float testShowTime;
    public float timer;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;
        if (timer > testShowTime)
        {
            GetComponent<Text>().enabled = false;
        }
		
	}
}
