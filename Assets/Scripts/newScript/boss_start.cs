using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boss_start : MonoBehaviour {
    [SerializeField]Boss boss;
    public bool start;
    [SerializeField] GameObject[] Cube;

	// Use this for initialization
	void Start () {
        boss.animator.speed = 0;
        start = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (boss.dead)
        {
            for (int i = 0; i < Cube.Length; i++)
                Cube[i].SetActive(false);
        }
		
	}
    private void OnTriggerEnter(Collider other)
    {
        if(!start && other.GetComponent<Player>())
        {
            boss.animator.Play("Idle");
            boss.animator.speed = 1;
            start = true;
            for (int i = 0; i < Cube.Length; i++)
                Cube[i].SetActive(true);
        }
    }
}
