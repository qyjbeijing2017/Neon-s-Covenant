using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReplayerColider : MonoBehaviour {
    FollowCapsule followCapsule;

	// Use this for initialization
	void Start () {
        followCapsule = FindObjectOfType<FollowCapsule>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Player>())
        {
            followCapsule.RePlayer();
        }
    }
}
