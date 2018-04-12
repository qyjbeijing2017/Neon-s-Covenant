using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MonsterBehaviour))]
[RequireComponent(typeof(MonsterAnim))]
[RequireComponent(typeof(CapsuleCollider))]
public class MonsterProperty : CProperty
{

	void Awake()
	{
		base.Awake();
		level.MonsterRegister();
	}

	void Update()
	{
		if (mainColorValue <= 0)
			anim.PlayAnim("Die");
		Debug.LogError("小怪死了");
	}
}
