using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBehaviour : CBehaviour
{
	UnityEngine.AI.NavMeshAgent m_Agent;
	private GameObject mainCharacter;
	[SerializeField] protected bool isChasing = false;
	[SerializeField] protected bool isLooking = false;
	[SerializeField] protected bool isAttacking = false;
	[SerializeField] protected float sensorRange = 20f;
	[SerializeField] protected float weakCD = 5f;
	//标记是否正在近战三连
	[SerializeField] protected bool isAttackingNear = false;
	//虚弱状态
	[SerializeField] protected bool isWeak = false;
	//每一次决策的间隔时间
	[SerializeField] protected float 决策时间 = 3.0f;
	[SerializeField] protected GameObject shield;
	[SerializeField] protected int 近战攻击1颜色;
	[SerializeField] protected int 近战攻击2颜色;
	[SerializeField] protected int 近战攻击3颜色;
	[SerializeField] protected float 激光DPS;
	[SerializeField] protected float 激光持续时间;
	[SerializeField] protected float 激光跟踪速度;
	public float 角度;
	public AttackNear rightHand;
	public AttackNear leftFoot;


	//行动=行走、攻击、虚弱等
	protected bool isActing = false;

	protected bool bActivated = false;    //被激活后角色离开碰撞体也不会停止行动

	[SerializeField] private GameObject bossCopy;

	protected bool bSeparated = false;

	protected void Awake()
	{
		base.Awake();

		m_Agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
		mainCharacter = GameObject.FindGameObjectWithTag("MainCharacter");

	}

	void Update()
	{
		if (isChasing)
		{
			Move(mainCharacter.transform.position);
		}
		else
			m_Agent.isStopped = true;
		//处于攻击距离中，且不在行动
		if (IsNearCharacter() && !isAttacking && isChasing)
		{
			isChasing = false;
			//Debug.Log("停止追击");
			m_Agent.isStopped = true;
			AttackNear1();
			isAttacking = true;
			StopCoroutine("DecisionCR");
		}

		if ((mainCharacter.transform.position - transform.position).magnitude <= sensorRange && !bActivated)
		{
			bActivated = true;
			//Debug.Log("Character sensored");
			Decide();
		}

		shield.GetComponent<Renderer>().material.color = ((property as BossProperty).shieldColor == 1) ? Color.red : Color.cyan;

	}
	public override void GetHit(int color, float value, int typeOfAttack)
	{
		//检查是否处于状态Ⅰ
		if ((property as BossProperty).atStateOne)
		{
			if ((property as BossProperty).shield == 0)
			{
				if (isAttackingNear)    //近战连击中
				{
					//Debug.Log("近战中受到攻击");
					switch (typeOfAttack)
					{
						//反色近战
						case 0: if (color == 7 || color == -(property as BossProperty).shieldColor) GetWeak(); break;  //注意，这个地方意味着需要将攻击的颜色存储在shieldColor变量中，才能进行这样的判断
																													   //远程攻击
						case 2:
							{
								property.mainColorValue -= value * (property as BossProperty).reductionOfDamageDistant;
								if (property.mainColorValue <= (property as BossProperty).turnStateAt)
									SetStateTwo();
								StartCoroutine(DecisionCR());
							}; break;
					}
				}
				else if (isWeak) //虚弱状态
				{

					property.mainColorValue -= value * (property as BossProperty).onWeakMultiplier;
					//Debug.Log("Very Very Weak and the hurt is " + value);
					if (property.mainColorValue <= (property as BossProperty).turnStateAt)
						SetStateTwo();
					StartCoroutine(DecisionCR());
				}
				else //非虚弱、非近战中
				{
					property.mainColorValue -= value;
					//Debug.Log(property.mainColorValue);
					if (property.mainColorValue <= (property as BossProperty).turnStateAt)
						SetStateTwo();
				}
			}
			else//有盾
			{
				if ((property as BossProperty).shieldColor == color)
				{
					//Debug.Log("盾满");
					(property as BossProperty).shield = 2;
				}
				else
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
			}
		}
		//状态Ⅱ
		else
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
	}

	/// <summary>
	/// AI决策
	/// </summary>
	private void Decide()
	{
		if ((property as BossProperty).atStateOne)
		{
			//Debug.Log("State One");
			//不虚弱
			if (!isWeak)
			{
				//Debug.Log("不虚弱");
				//不在行动
				if (!isActing)
				{
					//Debug.Log("不在行动");
					//靠近玩家
					if (IsNearCharacter())
					{
						//Debug.Log("在角色附近");
						isAttacking = true;
						//调用三连击的第一段
						AttackNear1();
					}
					else
					{
						//Debug.Log("不在角色附近");
						//标记为正在行动
						isActing = true;
						//标记为正在追逐
						isChasing = true;
						//Debug.Log("开始追击");
						m_Agent.isStopped = false;

						StartCoroutine("DecisionCR");
					}
				}
				//已经在行动中
				else
				{
					//Debug.Log("已经在行动");
					int rd = Random.Range(0, 3);
					isChasing = false;
					m_Agent.isStopped = true;
					switch (rd)
					{
						case 0: AttackNearFlash(); break;
						case 1: AttackDistant(); break;
						case 2: AttackLaser(); break;
					}
				}
			}
			else
			{
				//Debug.Log("虚弱");
			}

		}
		//状态Ⅱ
		else
		{
			//Debug.Log("State 2");
			int rd = Random.Range(0, 3);
			switch (rd)
			{
				case 0: AttackNearFlash(); break;
				case 1:
					{
						AttackDistant();
						Seperate("dis");
					}
					break;
				case 2:
					{
						AttackLaser();
						Seperate("laser");
					}
					break;
			}
		}
	}

	#region 战斗相关的函数 
	/// <summary>
	/// 闪现三连击
	/// </summary>
	private void AttackNearFlash()
	{
		//Debug.Log("Flash!");
		transform.position = mainCharacter.transform.position + mainCharacter.transform.forward * 2;
		transform.forward = mainCharacter.transform.position - transform.position;
		AttackNear1();
		StopCoroutine("DecisionCR");
	}

	/// <summary>
	/// 三连击第一段
	/// </summary>
	private void AttackNear1()
	{
		//进入无敌状态
		//Debug.Log("进入无敌状态，并开始近战第一段");

		shield.GetComponent<Renderer>().enabled = false;
		isAttacking = true;
		StopCoroutine("DecisionCR");
		transform.forward = mainCharacter.transform.position - transform.position;
		isAttackingNear = true;
		anim.PlayAnim("NearAttack1");

		(property as BossProperty).shieldColor = 1;

	}

	/// <summary>
	/// 三连击第二段
	/// </summary>
	public void AttackNear2()
	{
		//Debug.Log("近战第二段");
		transform.forward = mainCharacter.transform.position - transform.position;
		AttackNearReset();
		anim.PlayAnim("NearAttack2");
		(property as BossProperty).shieldColor = -1;

	}

	/// <summary>
	/// 三连击第三段
	/// </summary>
	public void AttackNear3()
	{
		//Debug.Log("近战第三段");
		transform.forward = mainCharacter.transform.position - transform.position;
		AttackNearReset();
		anim.PlayAnim("NearAttack3");
	}

	/// <summary>
	/// 远程攻击
	/// </summary>
	protected virtual void AttackDistant()
	{
		//Debug.Log("远程攻击");
		isChasing = false;
		StopCoroutine("DecisionCR");
		anim.PlayAnim("RangeAttack");
	}

	/// <summary>
	/// 激光攻击
	/// </summary>
	protected virtual void AttackLaser()
	{
		//Debug.Log("激光攻击");
		isChasing = false;
		StopCoroutine("DecisionCR");
		anim.PlayAnim("LaserAttack");
		int a = 0;
		switch (Random.Range(0, 2))
		{
			case 0: a = -1; break;
			case 1: a = 1; break;
		}
		Debug.LogError(a);
		StartCoroutine(激光(a));
	}

	/// <summary>
	/// 护盾
	/// </summary>
	private void ShielUp(int color)
	{
		(property as BossProperty).shield = 2;
		(property as BossProperty).shieldColor = color;
		shield.GetComponent<Renderer>().enabled = true;
		StartCoroutine(DecisionCR());
	}

	/// <summary>
	/// 进入虚弱状态
	/// </summary>
	protected virtual void GetWeak()
	{
		//护盾清空
		(property as BossProperty).shield = 0;
		isWeak = true;
		isChasing = false;
		//PlayAnimationHere
		shield.GetComponent<Renderer>().enabled = false;

		StopAllCoroutines();
		StartCoroutine(RecoverFromWeak());
	}

	public void AttackDistantLaunch()
	{
		Projectile b = Instantiate(bullet, spawnPlace.position, Quaternion.identity).GetComponent<Projectile>();
		transform.localEulerAngles += new Vector3(0, 角度, 0);
		b.SetProperty(1, attackValueDistant, this.name);
		b.GetComponent<Rigidbody>().velocity = transform.forward * attackFlySpeed;

		b = Instantiate(bullet, spawnPlace.position, Quaternion.identity).GetComponent<Projectile>();
		transform.localEulerAngles += new Vector3(0, -2 * 角度, 0);
		b.SetProperty(-1, attackValueDistant, this.name);
		b.GetComponent<Rigidbody>().velocity = transform.forward * attackFlySpeed;

		b = Instantiate(bullet, spawnPlace.position, Quaternion.identity).GetComponent<Projectile>();
		transform.localEulerAngles += new Vector3(0, 角度, 0);
		b.SetProperty(7, attackValueDistant, this.name);
		b.GetComponent<Rigidbody>().velocity = transform.forward * attackFlySpeed;

		(property as BossProperty).shieldColor = -(property as BossProperty).shieldColor;
	}

	public void AttackLaserLaunch()
	{

	}

	/// <summary>
	/// 结束攻击状态
	/// </summary>
	public virtual void AttackEnd()
	{
		//Debug.Log("Attack end");
		isActing = false;
		isAttacking = false;
		isAttackingNear = false;
		if (bSeparated)
			DeSeparate();

		AttackNearReset();

		StartCoroutine(DecisionCR());
	}

	public virtual void AttackEnd(bool a)
	{
		//Debug.Log("Attack end");
		isActing = false;
		isAttacking = false;
		isAttackingNear = false;
		if (bSeparated)
			DeSeparate();

		AttackNearReset();
		shield.GetComponent<Renderer>().enabled = true;
		StartCoroutine(DecisionCR());
	}
	#endregion

	public virtual void AttackNearLaunch(int sec)
	{
		switch (sec)
		{
			case 0:
				{
					rightHand.SetProperty(1, attackValueNear);
					rightHand.GetComponent<Collider>().enabled = true;
				}
				break;
			case 1: { rightHand.SetProperty(-1, attackValueNear); rightHand.GetComponent<Collider>().enabled = true; } break;
			//介里是jio的攻击力
			case 2: { leftFoot.SetProperty(7, 50); leftFoot.GetComponent<Collider>().enabled = true; } break;
		}
	}

	public void AttackNearReset()
	{
		rightHand.SetProperty(0, 0);
		rightHand.GetComponent<Collider>().enabled = false;

		leftFoot.SetProperty(0, 0);
		leftFoot.GetComponent<Collider>().enabled = false;
	}
	/// <summary>
	/// DecisionC(o)R(outine)，需要在攻击相关的函数中关闭该协程
	/// </summary>
	/// <returns></returns>
	IEnumerator DecisionCR()
	{
		//Debug.Log("start coroutine");
		//其实我希望在这个timeToDecide这里有个波动值
		yield return new WaitForSeconds(决策时间);
		//Debug.Log("进行了一次决策");
		Decide();
	}

	IEnumerator RecoverFromWeak()
	{
		yield return new WaitForSeconds(weakCD);
		isWeak = false;
		if (Random.Range(0, 2) > 1)
		{
			ShielUp(-1);
		}
		else
			ShielUp(1);
	}


	/// <summary>
	/// 切换到第二状态
	/// </summary>
	private void SetStateTwo()
	{
		//Debug.Log("Switch to Phase 2");
		(property as BossProperty).atStateOne = false;
	}

	/// <summary>
	/// 分裂出一个分身
	/// </summary>
	protected void Seperate(string type)
	{
		property.level.savedBossPosition = transform.position;
		BossProperty temp = Instantiate(bossCopy, transform.position + transform.forward * 3, Quaternion.identity).GetComponent<BossProperty>();
		BossCopy tempb = temp.GetComponent<BossCopy>();
		temp.SetProperty(
			property.mainColorValue, true,
					(property as BossProperty).shield,
							(property as BossProperty).shieldColor, false, true, this.gameObject
			);
		tempb.bActivated = true;
		tempb.isChasing = true;
		bSeparated = true;
		tempb.bSeparated = true;
		if (type == "laser")
			tempb.AttackLaser();
		else
			tempb.AttackDistant();
		//嘤嘤嘤我要师妹
	}

	protected virtual void DeSeparate()
	{
		transform.position = property.level.savedBossPosition;
		
	}

	/// <summary>
	/// 计算分身1的位置
	/// </summary>
	/// <returns></returns>
	private Vector3 GenPos1()
	{
		return Vector3.zero;
	}

	/// <summary>
	/// 计算分身2的位置
	/// </summary>
	/// <returns></returns>
	private Vector3 GenPos2()
	{
		return Vector3.zero;

	}

	/// <summary>
	/// 返回追逐状态（用于更新动画）
	/// </summary>
	/// <returns></returns>
	public bool IsChasing() { return isChasing; }

	/// <summary>
	/// 直接用了获取到的Position，没有将Y轴归零，会有偏差
	/// </summary>
	/// <returns></returns>
	bool IsNearCharacter()
	{
		if ((transform.position - mainCharacter.transform.position).magnitude < (property as BossProperty).attackRange)
			return true;
		else
			return false;
	}

	/// <summary>
	/// 利用自动寻路实现追逐玩家的效果
	/// </summary>
	/// <param name="trans"></param>
	protected override void Move(Vector3 trans) { m_Agent.SetDestination(trans); }

	protected IEnumerator 激光(int color)
	{
		Vector3 stalker = mainCharacter.transform.position;
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

		AttackEnd();
		laserLineRender.enabled = false;

	}
}
