using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stop_test : MonoBehaviour {

    [SerializeField] KeyCode stop;
    [SerializeField] float stopTime;
    [SerializeField] int damage;
    [SerializeField] float powerDamage;
    [SerializeField] int damageType;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(stop))
        {
            FindObjectOfType<Player_new>().inJured(damage, powerDamage, damageType, stopTime);
        }
		
	}
}
