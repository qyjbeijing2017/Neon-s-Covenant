using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_ReadPlayer : MonoBehaviour {
    [SerializeField]KeyCode readKey;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(readKey))
        {
            FindObjectOfType<Player_save>().ReadPlayer();
        }
	}
}
