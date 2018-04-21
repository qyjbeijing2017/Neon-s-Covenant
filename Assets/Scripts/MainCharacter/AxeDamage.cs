using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeDamage : MonoBehaviour {

    public int axeDamege;
    public int axeType;
    public float stopTime;

	// Use this for initialization
	void Start () {
        GetComponent<Collider>().enabled = false;
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
		print(other.name);
        if(other.tag == "Boss")
        {
            if (other.GetComponent<Boss_new>().shield <= 0 || other.GetComponent<Boss_new>().specialAttack)
            {
                other.GetComponent<Boss_new>().injured(axeDamege, axeType);
            }
        }
        else if(other.tag == "Enemy")
        {
            other.GetComponent<Enemy>().injured(axeDamege, axeType, stopTime);
        }
    }

}
