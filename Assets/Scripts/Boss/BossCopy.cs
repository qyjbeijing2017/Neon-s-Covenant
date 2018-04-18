using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCopy : BossBehaviour
{

	void Awake()
	{
		property = GetComponent<CProperty>();
		anim = GetComponent<CAnim>();
		anim = gameObject.AddComponent<BossAnim>();
		mainCharacter = GameObject.FindGameObjectWithTag("MainCharacter");

	}

	void Update()
	{
		shield.GetComponent<Renderer>().material.color = ((property as BossProperty).shieldColor == 1) ? new Color(1, 0, 0, (float)0.4) : new Color(0, 1, 1, (float)0.4);
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
			if (typeOfAttack == 2)
			{
				if ((property as BossProperty).shieldColor != color && color != 0)
				{
					if ((property as BossProperty).shield == 1)
					{
						//Debug.Log("破盾");
						GetWeak();
					}
					else
					{
						//Debug.Log("盾-1");
						(property as BossProperty).shield = 1;
					}
				}
				else
				{
					//Debug.Log("盾满");
					(property as BossProperty).shield = 2;
				}
			}
		}
	}

	public override void GetWeak()
	{
		Debug.LogError("分身虚弱 번선흐로");
		DeSeparate(true);
	}

	protected override void DeSeparate(bool getWeak)
	{
		(property as BossProperty).real.GetComponent<BossProperty>().mainColorValue = property.mainColorValue;
		if (getWeak)
		{
			(property as BossProperty).real.GetComponent<BossBehaviour>().GetWeak();
		}
		else
		{
			(property as BossProperty).real.GetComponent<BossProperty>().shield =
((property as BossProperty).real.GetComponent<BossProperty>().shield > (property as BossProperty).shield) ?
(property as BossProperty).real.GetComponent<BossProperty>().shield : (property as BossProperty).shield;
		}
		Destroy(this.gameObject);

	}

	/// <summary>
	/// 远程攻击
	/// </summary>
	protected override void AttackDistant()
	{

		isChasing = false;
		StopCoroutine("DecisionCR");
		anim.PlayAnim("RangeAttack");
	}

	/// <summary>
	/// 激光攻击
	/// </summary>
	protected override void AttackLaser()
	{
		Debug.Log("激光攻击");
		isChasing = false;
		StopCoroutine("DecisionCR");
		anim.PlayAnim("Boss_laser_start");
	}

	public override void AttackEnd()
	{
		base.AttackEnd();
		DeSeparate(false);
	}

	public override void AttackNearReset()
	{

	}

	public IEnumerator 激光(int color)
	{
		Debug.LogError(gameObject.name);

		Transform tempT;

		tempT= mainCharacter.transform;//获取主角的位置

		Vector3 stalker = mainCharacter.transform.position - tempT.forward*激光起始偏移因数;

		float timer = 激光持续时间;
		LineRenderer laserLineRender = GetComponent<LineRenderer>();
		laserLineRender.enabled = true;
		while (timer >= 0)
		{
			stalker += (mainCharacter.transform.position - stalker).normalized * 激光跟踪速度 * Time.deltaTime;
			transform.forward = stalker - transform.position;
			timer -= Time.deltaTime;
			Ray ray = new Ray(transform.position, stalker - transform.position);
			RaycastHit hit;
			if (Physics.Raycast(ray, out hit, 30f))
			{

				(property as BossProperty).shieldColor = color;
				laserLineRender.SetPosition(0, this.transform.position + (0.3f * transform.forward));

				//print(hit.collider.gameObject.name);
				if ((hit.point - transform.position).magnitude < (stalker - transform.position).magnitude)
				{
					laserLineRender.SetPosition(1, hit.point);
					if (hit.collider.tag == "MainCharacter")
						hit.collider.GetComponent<CBehaviour>().GetHit(color, 激光DPS, 2);
				}
				else
				{
					laserLineRender.SetPosition(1, stalker);
				}
				//print(hit.collider.name);
			}

			yield return 0;
		}
	}
}