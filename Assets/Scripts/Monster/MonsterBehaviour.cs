using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(UnityEngine.AI.NavMeshAgent))]
public class MonsterBehaviour : CBehaviour
{
	UnityEngine.AI.NavMeshAgent m_Agent;

	private MainCharacterProperty mainCharacter;

	[SerializeField] private float attackRange = 1;
	/// <summary>
	/// 攻击冷却时间以秒计算，可能由动画控制代替
	/// </summary>
	[SerializeField] private float attackCD = 1;
	[SerializeField] private float anti_ColorMultiplier = 1.5f;

	[SerializeField] private bool isChasing = false;
	[SerializeField] private bool isLooking = false;
	[SerializeField] private bool isAttacking = false;


	// Use this for initialization
	void Awake()
	{
		base.Awake();

		m_Agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
		mainCharacter = GameObject.FindGameObjectWithTag("MainCharacter").GetComponent<MainCharacterProperty>();

	}

	void Update()
	{
		//if (isChasing)
		//{
		//	if (IsNearCharacter())
		//	{
		//		anim.PlayAnim("Attack");
		//		isChasing = false;
		//	}
		//	else
		//	{
		//		Move(mainCharacter.transform.position);
		//		anim.SetMovingAnim(true);
		//	}
		//}


		if (isChasing) {
			if (!isAttacking)
			{
				if (IsNearCharacter())
				{
					anim.PlayAnim("Attack");
					isChasing = false;
				}
				else
				{
					Move(mainCharacter.transform.position);
				}
			}
		}
		anim.SetMovingAnim(isChasing);

	}

	public override void GetHit(int color, float value, int typeOfAttack)
	{
		if (property.mainColor != color)
		{
			property.mainColorValue -= value;
			if (typeOfAttack == 0)
				anim.PlayAnim("Delay");
		}
	}

	public void AttackDistant()
	{
		isAttacking = true;
		GameObject t = Instantiate(bullet, spawnPlace.position, Quaternion.identity);
		t.GetComponent<Rigidbody>().velocity = attackFlySpeed * transform.forward;

	}

	public void AttackNear()
	{
		weapon.SetProperty(property.mainColor, attackValueNear);
	}

	public void AttackReset()
	{
		if (weapon)
			weapon.GetComponent<AttackNear>().SetProperty(property.mainColor, attackValueNear);
	}

	public void AttackEnd()
	{
		isChasing = true;
	}

	public void Move(Vector3 trans)
	{
		m_Agent.SetDestination(trans);
	}

	bool IsNearCharacter()
	{
		if ((transform.position - mainCharacter.transform.position).magnitude <attackRange)
			return true;
		else
			return false;
	}

	//进入监视范围
	void OnTriggerEnter()
	{
		isChasing = true;
	}

	//可以跑出监视范围
	void OnTriggerExit()
	{
		//isChasing = false;
	}
}
