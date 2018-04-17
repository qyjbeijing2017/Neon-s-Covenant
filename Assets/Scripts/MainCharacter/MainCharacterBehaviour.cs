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


	public SpriteRenderer aimLine;

	public float linedis;
	[SerializeField] private float 硬直时间;  // yingzhiShijian;

	public UnityEngine.UI.Slider sliderWhite;
	public UnityEngine.UI.Slider sliderOther;
	public UnityEngine.UI.Image img;

	bool attacking;
	bool dashNow;

	//初始化Property和Anim
	void Start()
	{
		m_property = GetComponent<MainCharacterProperty>();
		m_anim = GetComponent<MainCharacterAnim>();

		lastPosition = transform.position;

		dashNow = false;

		FlushUI();
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
			tag = "InvincibleMainCharacter";

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


			Vector3 offset = new Vector3((hit.point - transform.position).x, 0, (hit.point - transform.position).z);

			if (offset.magnitude > 0.7)
			{
				transform.forward = new Vector3(offset.x, transform.forward.y, offset.z).normalized;
			}



			aimLine.size = new Vector2((hit.point - this.transform.position).magnitude, aimLine.size.y);
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
			FlushUI();
		}
		else if (m_property.otherColorValue > 0)
		{
			temp.GetComponent<Projectile>().SetProperty(m_property.otherColor, attackValueDistant, name);
			m_property.mainColorValue -= (10 - m_property.otherColorValue);
			m_property.otherColorValue = 0;

		}
		else
		{
			temp.GetComponent<Projectile>().SetProperty(m_property.mainColor, attackValueDistant, this.name);
			m_property.mainColorValue -= 10;
			FlushUI();
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
				FlushUI();
			}
			else
			{
				m_property.mainColorValue -= (value - m_property.otherColorValue);
				m_property.otherColorValue = 0;

				FlushUI();
				Debug.Log("MainC: " + property.mainColor + "  MainV: " + property.mainColorValue + "  OtherC: " + property.otherColor + "  OtherV: " + property.otherColorValue);
			}
		}
		else if (m_property.otherColor == color)
		{
			if (m_property.mainColorValue > value)
			{
				m_property.mainColorValue -= value;
				m_property.otherColorValue += value;
				Debug.Log("MainC: " + property.mainColor + "  MainV: " + property.mainColorValue + "  OtherC: " + property.otherColor + "  OtherV: " + property.otherColorValue);
				FlushUI();
			}
			else
				Die();
		}
		else
		{
			if (m_property.otherColorValue >= value)			//一个等号引发的血案emmmm
			{
				m_property.otherColorValue -= value;
				m_property.mainColorValue += value;
				Debug.Log("MainC: " + property.mainColor + "  MainV: " + property.mainColorValue + "  OtherC: " + property.otherColor + "  OtherV: " + property.otherColorValue);
				FlushUI();
			}
			else
			{
				m_property.mainColorValue = m_property.mainColorValue - value + m_property.otherColorValue;
				m_property.otherColorValue = value - m_property.otherColorValue;
				m_property.otherColor = color;

				Debug.Log("MainC: " + property.mainColor + "  MainV: " + property.mainColorValue + "  OtherC: " + property.otherColor + "  OtherV: " + property.otherColorValue);
				FlushUI();
			}
		}

		if (typeOfAttack ==1)
		{
			StopCoroutine("硬直");
			StartCoroutine("硬直");
		}
	}

	public override void Die()
	{
		base.Die();
		Debug.Log("MainCharacter dies");
		Time.timeScale = 0.0f;
		//Destroy(this.gameObject);
	}

	protected void FlushUI()
	{
		sliderWhite.value = property.mainColorValue;
		sliderOther.value = property.mainColorValue + property.otherColorValue;
		img.color = property.otherColor == -1 ? Color.cyan : Color.red;

	}

	//翻滚位移
	IEnumerator Dashing()
	{

		while (isAttacking)
		{
			transform.position += transform.forward * DashSpeed * Time.deltaTime;
			yield return 0;
		}
		tag = "MainCharacter";
		yield return new WaitForSeconds(dashCD);
		dashNow = false;
	}

	IEnumerator 硬直()
	{
		anim.PlayAnim("硬直");
		isAttacking = true;
		yield return new WaitForSeconds(硬直时间);
		isAttacking = false;
		anim.PlayAnim("Idle");
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
