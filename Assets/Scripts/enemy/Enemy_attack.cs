using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_attack : MonoBehaviour {

    [SerializeField] Enemy enemy;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "MainCharacter")
        {
            enemy.attack = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "MainCharacter")
        {
            enemy.attack = false;
        }
    }

}
