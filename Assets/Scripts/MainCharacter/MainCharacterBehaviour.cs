using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MainCharacterBehaviour : CBehaviour
{
	MainCharacterProperty m_property;
	MainCharacterAnim m_anim;
	AimHelper aimHelper;
	[SerializeField] public bool isAttacking = false;

	float speedX = 0, speedZ = 0;

	Vector3 lastPosition;

	public float DashSpeed;

	public AttackNear attackNear;

	public float dashCD;

	[HideInInspector] public LineRenderer aimLine;

	public float linedis;


	bool attacking;
	bool dashNow;

	//初始化Property和Anim
	void Start()
	{
		m_property = GetComponent<MainCharacterProperty>();
		m_anim = GetComponent<MainCharacterAnim>();

		lastPosition = transform.position;

		dashNow = false;

		aimLine = GetComponent<LineRenderer>();
	}

	//监听攻击
	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Space))
		{
			Dash();
		}

		if (Input.GetMouseButton(1) && !isAttacking && !dashNow)
		{
			AttackDistantAim();
		}

		if (Input.GetMouseButtonUp(1))
		{
			aimLine.enabled = false;
		}

		if (Input.GetMouseButtonDown(0) && !isAttacking && !dashNow)
		{
			AttackNear();
		}
		lastPosition = transform.position;



	}
	//监听移动
	void FixedUpdate()
	{
		Move();
	}

	//实现移动（直接修改位置）
	protected override void Move()
	{
		m_anim.SetMovingAnim(false);
		if (!isAttacking)
		{
			if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S))
			{
				if (Input.GetKey(KeyCode.S)) speedZ = -1;
				else speedZ = 1;
			}
			else speedZ = 0;
			if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
			{
				if (Input.GetKey(KeyCode.D))
					speedX = 1;
				else
					speedX = -1;
			}
			else speedX = 0;


			transform.position += new Vector3(speedX, 0, speedZ) * moveSpeed;
			if (speedX != 0 || speedZ != 0) m_anim.SetMovingAnim(true);
			else m_anim.SetMovingAnim(false);
		}

		if (transform.position.x != lastPosition.x || transform.position.z != lastPosition.z)
		{
			Vector3 v = new Vector3(transform.position.x - lastPosition.x, 0, transform.position.z - lastPosition.z);
			if (v != Vector3.zero)
			{
				transform.forward = v;
			}
		}

	}

	//实现前翻
	protected void Dash()
	{
		if (!dashNow)
		{
			Debug.Log("Dash to set isAttacking");
			isAttacking = true;
			dashNow = true;
			StartCoroutine(Dashing());
			//这个地方需要做一个插值的位移
			m_anim.PlayAnim("Roll");

		}

	}

	//右键显示瞄准器
	protected override void AttackDistantAim()
	{
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;
		LayerMask mouseMask = 1 << 9;

		if (Physics.Raycast(ray, out hit, 100f, mouseMask))
		{
			#region
			//// 目标偏移  其中Y轴已归零
			Vector3 offset = new Vector3((hit.point - transform.position).x, 0, (hit.point - transform.position).z);

			////更新辅助瞄准器  值得注意的是这里transform.position的Y轴坐标极有可能是0 不过无伤大雅
			//aimHelper.Draw(transform.position, offset);

			//////根据当前面向，计算转到面向目标所需要的角度
			//float angle = Vector3.Angle(transform.forward, offset);
			////// 计算面向目标大约所需要的时间  
			////float needTime = angle / 30;
			////// 旋转进度（第三参数）  
			////float v1 = 1;
			//////如果所需要的时间接近0，则直接转到位，否则重新计算进度。  
			////if (needTime > Mathf.Epsilon) v1 = Time.deltaTime / needTime;
			////// 目标Rotation  
			////Quaternion t = Quaternion.LookRotation(offset);
			////// 进行旋转  
			////transform.rotation = t;
			////Quaternion.Slerp(transform.rotation, t, v1);
			//transform.Rotate(0, angle, 0);
			#endregion

			transform.forward = new Vector3(offset.x, transform.forward.y, offset.z).normalized;
			aimLine.SetPosition(0, transform.position + (transform.forward * linedis));
			aimLine.SetPosition(1, new Vector3(hit.point.x, transform.position.y, hit.point.z));
			aimLine.enabled = true;



		}

		//再按下左键
		if (Input.GetMouseButtonDown(0))
		{
			AttackDistant();
		}
	}

	//右键后左键进行远程攻击
	protected override void AttackDistant()
	{
		isAttacking = true;
		m_anim.PlayAnim("AttackDistant");
	}

	//左键近战攻击
	protected override void AttackNear()
	{

		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;
		LayerMask mouseMask = 1 << 9;

		if (Physics.Raycast(ray, out hit, 100f, mouseMask))
		{

			Vector3 offset = new Vector3((hit.point - transform.position).x, 0, (hit.point - transform.position).z);

			transform.forward = new Vector3(offset.x, transform.forward.y, offset.z).normalized;
			//Debug.Log("AttackNear to set true");
			isAttacking = true;
			if (m_property.otherColorValue != 0)
				weapon.GetComponent<AttackNear>().SetProperty(m_property.otherColor, attackValueNear);
			else
				weapon.GetComponent<AttackNear>().SetProperty(m_property.mainColor, attackValueNear);

			m_anim.PlayAnim("AttackNear");
		}
	}

	public void AttackDistantLaunch()
	{
		GameObject temp = Instantiate(bullet, spawnPlace.position + transform.forward, Quaternion.identity);
		if (m_property.otherColorValue > 10)
		{
			temp.GetComponent<Projectile>().SetProperty(m_property.otherColor, attackValueDistant, this.name);
			m_property.otherColorValue -= 10;
		}
		else
		{
			temp.GetComponent<Projectile>().SetProperty(m_property.mainColor, attackValueDistant, this.name);
			m_property.mainColorValue -= 10;
		}
		temp.GetComponent<Rigidbody>().velocity = transform.forward * attackFlySpeed;
		Debug.Log("Launch to set false");
		isAttacking = false;
	}

	//攻击后摇中取消硬直
	public void AttackEnd()
	{
		attackNear.GetComponent<Collider>().enabled = false;
		isAttacking = false;
	}

	public override void GetHit(int color, float value, int typeOfAttack)
	{
		if (color == 7)//黑色
		{
			if (m_property.otherColorValue >= value)

			{
				m_property.otherColorValue -= value;
				Debug.Log("MainC: " + property.mainColor + "  MainV: " + property.mainColorValue + "  OtherC: " + property.otherColor + "  OtherV: " + property.otherColorValue);
			}
			else
			{
				m_property.mainColorValue -= (value - m_property.otherColorValue);
				m_property.otherColorValue = 0;

				Debug.Log("MainC: " + property.mainColor + "  MainV: " + property.mainColorValue + "  OtherC: " + property.otherColor + "  OtherV: " + property.otherColorValue);
			}
		}
		else if (m_property.otherColor == color)
		{
			if (m_property.mainColorValue > value)
			{
				m_property.mainColorValue -= value;
				Debug.Log("MainC: " + property.mainColor + "  MainV: " + property.mainColorValue + "  OtherC: " + property.otherColor + "  OtherV: " + property.otherColorValue);
			}
			else
				Die();
		}
		else
		{
			if (m_property.otherColorValue > value)
			{
				m_property.otherColorValue -= value;
				m_property.mainColorValue += value;
				Debug.Log("MainC: " + property.mainColor + "  MainV: " + property.mainColorValue + "  OtherC: " + property.otherColor + "  OtherV: " + property.otherColorValue);
			}
			else
			{
				m_property.mainColorValue = m_property.mainColorValue - value + m_property.otherColorValue;
				m_property.otherColorValue = value - m_property.otherColorValue;
				m_property.otherColor = color;

				Debug.Log("MainC: " + property.mainColor + "  MainV: " + property.mainColorValue + "  OtherC: " + property.otherColor + "  OtherV: " + property.otherColorValue);
			}
		}
	}

	public override void Die()
	{
		base.Die();
		Debug.Log("MainCharacter dies");
		Destroy(this.gameObject);
	}

	//翻滚位移
	IEnumerator Dashing()
	{

		while (isAttacking)
		{
			transform.position += transform.forward * DashSpeed * Time.deltaTime;
			yield return 0;
		}

		yield return new WaitForSeconds(dashCD);
		dashNow = false;
	}

	//攻击伤害
	public void Attacking()
	{

	}



	public void AttackStart()
	{
		attackNear.GetComponent<Collider>().enabled = true;
	}
}
