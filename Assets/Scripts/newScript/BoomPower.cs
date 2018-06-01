using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomPower : MonoBehaviour {
    NSC_Character character;
    [SerializeField,Tooltip("爆光能血量百分比")] float[] HPper;
    [SerializeField, Tooltip("光能")] LightPower lightPower;

	// Use this for initialization
	void Start () {
		if(GetComponent<NSC_Character>())
        {
            character = GetComponent<NSC_Character>(); 
        }
        else
        {
            Debug.LogError("不存在Character");
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
