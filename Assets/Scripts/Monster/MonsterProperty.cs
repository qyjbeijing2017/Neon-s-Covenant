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

		//巨瘠薄强
		(behaviour as MonsterBehaviour).Test();
	}
}
