using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_weapon : MonoBehaviour {
    public int damage;
    public float damagePower;
    public int damageType;
    public float damageStop;
    public Enemy_audio enemyAudio;

	// Use this for initialization
	void Start () {
        GetComponent<Collider>().enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "MainCharacter")
        {
            other.GetComponent<Player_new>().inJured(damage, damagePower, damageType, damageStop);
            enemyAudio.enemy_Audio(2);
        }
    }
}
