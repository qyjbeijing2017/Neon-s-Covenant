using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossUI : MonoBehaviour {
    [SerializeField] NSC_Character Boss;
    Slider HP;

	// Use this for initialization
	void Start () {
        HP = GetComponent<Slider>();
		
	}
	
	// Update is called once per frame
	void Update () {
        HP.value =(float)Boss.HP / (float)Boss.HPMax;
	}
}
