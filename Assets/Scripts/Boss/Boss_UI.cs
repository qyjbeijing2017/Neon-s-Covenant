using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss_UI : MonoBehaviour {

    [SerializeField]Boss_new boss;
    [SerializeField]Slider bossHp;

	// Use this for initialization
	void Start () {
        bossHp.value = boss.HP / boss.maxHP;
		
	}
	
	// Update is called once per frame
	void Update () {
        bossHp.value = boss.HP / boss.maxHP;
    }
}
