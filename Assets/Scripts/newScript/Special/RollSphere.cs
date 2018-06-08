using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollSphere : MonoBehaviour {
    Rigidbody rigidbody;
    [SerializeField]float minSpeed;
    [SerializeField]float maxSpeed;
    [SerializeField]float changeTime;
	// Use this for initialization
	void Start () {
        rigidbody = GetComponent<Rigidbody>();
        rigidbody.useGravity = false;
        rigidbody.angularVelocity = new Vector3(Random.Range(minSpeed, maxSpeed), Random.Range(minSpeed, maxSpeed), Random.Range(minSpeed, maxSpeed));
        StartCoroutine(ChangeAngle());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    IEnumerator ChangeAngle()
    {
        while (true)
        {
            yield return new WaitForSeconds(changeTime);

            rigidbody.angularVelocity = new Vector3(Random.Range(minSpeed, maxSpeed), Random.Range(minSpeed, maxSpeed), Random.Range(minSpeed, maxSpeed));
            yield return 0;
        }


    }
}
