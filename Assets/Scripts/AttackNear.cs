using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackNear : MonoBehaviour
{

	public Animator anim;
	public int attackColor;
	public float attackValue;
	public bool attackOther;
	public GameObject otherObject;
	public string 攻击目标;

	public void SetProperty(int color, float value)
	{
		attackColor = color;
		attackValue = value;

		attackOther = false;
	}

	void OnTriggerEnter(Collider collider)
	{
		//if (collider.name != "Ground" && collider.tag != "woodenStick")
		//{
		//	collider.GetComponent<CBehaviour>().GetHit(attackColor, attackValue, 0);
		//}
		Debug.Log("color: "+attackColor+" value: " +attackValue + name);
		if (collider.tag ==攻击目标)
		{
			collider.GetComponent<CBehaviour>().GetHit(attackColor, attackValue, 0);
		}


	}

	private void OnTriggerExit(Collider other)
	{

	}
}
