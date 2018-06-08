using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Attack))]
public class Test_Attack : MonoBehaviour {
    [SerializeField]NSC_Character.CharacterType characterType;
    Attack attack;
    [SerializeField] KeyCode pushDamage; 

	// Use this for initialization
	void Start () {
        attack = GetComponent<Attack>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(pushDamage))
        {
            if (characterType == NSC_Character.CharacterType.player)
            {
                FindObjectOfType<Player>().injured(attack);
            }
        }
	}
}
