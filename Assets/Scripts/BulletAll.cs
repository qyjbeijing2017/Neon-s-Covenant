using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletAll : MonoBehaviour {

	// Use this for initialization

    public enum type
    {
        MainCharacter = 0,
        Boss = 1,
        Enemy = 2

    }

    private void Awake()
    {
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
        transform.right = -GetComponent<Rigidbody>().velocity;
        //print(GetComponent<Rigidbody>().velocity);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        print(other.name);
        if (myType.ToString() != other.tag)
        {
            if (other.tag == "BossCopy")
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
            if(other.tag == "scene")
            {
                Destroy(gameObject);
            }

            
        }
    }
}
