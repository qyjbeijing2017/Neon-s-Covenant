using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomPower : MonoBehaviour {
    NSC_Character character;
    [SerializeField,Tooltip("爆光能血量百分比")] float[] HPper;
    [SerializeField, Tooltip("光能")] LightPower lightPower;
    [SerializeField, Tooltip("爆光数量")] float nubOfLight;

    int NowNumber;

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
        NowNumber = 0;
	}
	
	// Update is called once per frame
	void Update () {

        if(NowNumber < HPper.Length)
        {
            if(character.HP < HPper[NowNumber])
            {
                for(int i = 0; i < nubOfLight; i++)
                {
                    Instantiate(lightPower, transform.position, transform.rotation);
                }
                NowNumber++;
            }
        }
            
		
	}
}
