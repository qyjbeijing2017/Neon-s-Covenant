using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_follow : MonoBehaviour {
    [SerializeField] Boss_new boss;

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
            boss.follow = true;
            boss.boss_start();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "MainCharacter")
        {
            boss.follow = false;
            boss.StopAllCoroutines();
            boss.boss_stopImmediately();
        }
    }
}
