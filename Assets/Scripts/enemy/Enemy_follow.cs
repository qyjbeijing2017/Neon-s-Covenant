using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_follow : MonoBehaviour {
    [SerializeField] Enemy enemy;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "MainCharacter")
        {
            enemy.enemy_start();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "MainCharacter")
        {
            enemy.enemy_stopImmediately();
        }
    }
}
