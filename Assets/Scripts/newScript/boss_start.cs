using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boss_start : MonoBehaviour {
    [SerializeField]Boss boss;
    bool start;

	// Use this for initialization
	void Start () {
        boss.animator.speed = 0;
        start = false;
	}
	
	// Update is called once per frame
	void Update () {
        
		
	}
    private void OnTriggerEnter(Collider other)
    {
        if(!start && other.GetComponent<Player>())
        {
            boss.animator.Play("Idle");
            boss.animator.speed = 1;
            start = true;
        }
    }
}
