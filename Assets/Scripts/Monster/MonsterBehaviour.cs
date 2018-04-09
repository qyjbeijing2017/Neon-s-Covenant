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
		if (isLooking && !isAttacking)
		{
			Move(mainCharacter.transform.position);
		}
	}

	//进入监视范围
	void OnTriggerEnter()
	{
		isLooking = true;
	}
	//
	void OnTriggerExit()
	{
		isChasing = false;
	}

	protected override void Move(Vector3 trans)
	{
		m_Agent.SetDestination(trans);
	}

	public void Test()
	{
		Debug.Log("Here's a function! Don't forget to delete me!");
	}
}
