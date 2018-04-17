using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeDamage : MonoBehaviour {

    public float axeDamege;

	// Use this for initialization
	void Start () {
        GetComponent<Collider>().enabled = false;
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Enemy" || other.tag == "Boss")
        {

        }
    }

}
