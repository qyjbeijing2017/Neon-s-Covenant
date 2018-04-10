using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCopy : BossBehaviour
{

	void Awake()
	{
		base.Awake();
		Destroy(gameObject.GetComponent<CAnim>());
		anim = gameObject.AddComponent<BossAnim>();
		Debug.LogError(anim);
	}

	public override void GetHit(int color, float value, int typeOfAttack)
	{
		if ((property as BossProperty).shield == 0)
		{
			if (isAttackingNear)
			{
				switch (typeOfAttack)
				{
					//反色近战
					case 0:
						if (color == -(property as BossProperty).shieldColor) GetWeak()
						; break;
					//远程攻击
					case 2: property.mainColorValue -= value * (property as BossProperty).reductionOfDamageDistant; break;
				}
			}
			else
				(property as BossProperty).mainColorValue -= value;
		}
		else
		{
			if ((property as BossProperty).shieldColor == color)
			{
				(property as BossProperty).shield = 2;
			}
			else
			{
				if ((property as BossProperty).shield == 1)
				{
					GetWeak();
				}
				else
					(property as BossProperty).shield = 1;
			}
		}
	}

	protected override void GetWeak()
	{
		Debug.LogError("Weak");
		DeSeparate();
	}

	protected override void DeSeparate()
	{
		(property as BossProperty).real.GetComponent<BossProperty>().mainColorValue = property.mainColorValue;
		(property as BossProperty).real.GetComponent<BossProperty>().shield = ((property as BossProperty).real.GetComponent<BossProperty>().shield > (property as BossProperty).shield) ? (property as BossProperty).real.GetComponent<BossProperty>().shield : (property as BossProperty).shield;
		Debug.LogError("DeSeparate");
		Destroy(this.gameObject);

	}

	/// <summary>
	/// 远程攻击
	/// </summary>
	protected override void AttackDistant()
	{
		Debug.Log("远程攻击");
		isChasing = false;
		StopCoroutine("DecisionCR");
		anim.PlayAnim("RangeAttack");
		int a = 0;
		switch (Random.Range(0, 2))
		{
			case 0: a = -1; break;
			case 1: a = 1; break;
		}
		StartCoroutine(激光(a));
	}

	/// <summary>
	/// 激光攻击
	/// </summary>
	protected override void AttackLaser()
	{
		Debug.Log("激光攻击");
		isChasing = false;
		StopCoroutine("DecisionCR");
		anim.PlayAnim("LaserAttack");
	}

	public override void AttackEnd()
	{
		base.AttackEnd();
	}


}
