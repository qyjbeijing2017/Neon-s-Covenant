using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(NSC_Color))]
public class Attack : MonoBehaviour {
    [Header("伤害")]
    [Tooltip("造成多少伤害")]
    public int damage;
    [Tooltip("能量伤害、或者对怪物伤害类型")]
    public NSC_Color powerDamage;


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
