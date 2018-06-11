using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveColider : MonoBehaviour {
    [SerializeField]Player_save player_Save;


	// Use this for initialization
	void Start () {
        if (player_Save == null)
        {
            player_Save = FindObjectOfType<Player_save>();
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Player>())
        {
            player_Save.SavePlayer();
        }
    }
}
