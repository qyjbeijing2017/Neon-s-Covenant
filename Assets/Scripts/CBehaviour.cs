using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// CBehaviour类没有依赖
/// </summary>
public class CBehaviour : MonoBehaviour
{

	//Digits about Moving
	[SerializeField] public float moveSpeed;
	[SerializeField] protected float rotateSpeed;
	[SerializeField] protected float dashDistance;

	//Digits about attacking
	[SerializeField] public float attackValueDistant;
	[SerializeField] public float attackValueNear;
	[SerializeField] protected float attackFreq;
	[SerializeField] protected float attackFlySpeed;

	//Other
	[SerializeField] protected GameObject bullet;
	public AttackNear weapon;
	[SerializeField] protected Transform spawnPlace;

	protected CProperty property;
	protected CAnim anim;

	/// <summary>
	/// 进行组件的引用
	/// </summary>
	protected void Awake()
	{
		property = GetComponent<CProperty>();
		anim = GetComponent<CAnim>();
	}


	protected virtual void Move() { }
	//Move alone a vector, and this vector don't have to be normalized.
	protected virtual void Move(Vector3 trans) { transform.position += trans; }

	/// <summary>
	/// 此对象受伤，若为建筑，则反弹攻击
	/// </summary>
	public virtual void GetHit(int color, float value, int typeOfAttack) { }

	//暂时不知道会不会有治疗的存在
	public virtual void GetHealed(int color, float value) { }

	// 远距离攻击
	protected virtual void AttackDistant() { }
	protected virtual void AttackDistant(int color, float attackValueDistant, GameObject prefab, float attackFlySpeed, Vector3 spawnPosition, Vector3 v)
	{
		GameObject pref = Instantiate(prefab, spawnPosition, Quaternion.identity);
		pref.GetComponent<Rigidbody>().velocity = v;
		//为生成的实例赋初始值
		pref.GetComponent<Projectile>().SetProperty(color, attackValueDistant,this.name);
	}

	protected virtual void AttackDistantAim() { }

	//近距离攻击
	protected virtual void AttackNear() { }
	protected virtual void AttackNear(int color, float attackValueNear) { }

	public virtual void Die() { }
}
