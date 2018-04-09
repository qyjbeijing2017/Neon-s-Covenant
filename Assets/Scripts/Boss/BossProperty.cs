using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BossBehaviour))]
[RequireComponent(typeof(BossAnim))]
public class BossProperty : CProperty
{
	public float attackRange;   //攻击距离

	public float turnStateAt;   //低于该值则进入状态Ⅱ

	public int shield = 0;  //护盾值

	public int shieldColor = 0; //护盾颜色

	public bool atStateOne = true; //标记状态

	public bool isFake = false;  //分身标记

	public GameObject real;//真身（主体）

	//近战攻击减伤比
	public float reductionOfDamageNear;
	//远程攻击减伤比
	public float reductionOfDamageDistant;
	//虚弱状态下受伤害倍乘因数
	public float onWeakMultiplier;

	void Awake()
	{
		base.Awake();
		atStateOne = true;
		isFake = false;
	}

	public void SetProperty(float mColorValue, bool alive, int s, int sColor, bool stateOne, bool fake, GameObject realOne)
	{
		mainColorValue = mColorValue;
		isAlive = alive;
		shield = s;
		shieldColor = sColor;
		atStateOne = stateOne;
		isFake = fake;
		real = realOne;
	}


}
