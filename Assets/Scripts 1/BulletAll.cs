using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletAll : MonoBehaviour {

	// Use this for initialization

    public enum type
    {
        player = 0,
        boss = 1,
        enemy = 2

    }

    public type myType;
    public int damageHP;
    public float damagePower;
    public int colorType;
    public float speed;
    public float destoryTime;
    public float stopTime;
    [SerializeField] private float bulletVelocity;

    void Start () {

        Destroy(gameObject, destoryTime);
        GetComponent<Rigidbody>().velocity = GetComponent<Rigidbody>().velocity.normalized * bulletVelocity;


    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "BossCopy")
        {
            other.GetComponent<Boss_copy>().injured(damageHP, colorType);
            Destroy(gameObject);
        }
        if (other.tag == "Boss")
        {
            other.GetComponent<Boss_new>().injured(damageHP, colorType);
            Destroy(gameObject);
        }
        if (other.tag == "MainCharacter")
        {
            other.GetComponent<Player_new>().inJured(damageHP, damagePower, colorType, stopTime);
            Destroy(gameObject);
        }
        

    }
}
