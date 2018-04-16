using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
	Animator anim;
	public int pColor = 0;
	public float attackValue = 0;
	public string launcherName = null;

	public void SetProperty(int color, float value, string n)
	{
		pColor = color; 
		attackValue = value;
		launcherName = n;

        switch (color)
        {
            case 0:
                GetComponent<Renderer>().material.color = Color.white; break;
            case 1:
                GetComponent<Renderer>().material.color = Color.red; break;
            case -1:
                GetComponent<Renderer>().material.color = Color.cyan; break;
            case 7:
                GetComponent<Renderer>().material.color = Color.black; break;
        }
    }

	//子弹本身没有碰撞，只有一个trigger
	void OnTriggerEnter(Collider collider)
	{
		Debug.Log(collider.name);
		if (collider.tag == "MainCharacter" &&collider.name != launcherName|| collider.tag == "Enemy" && collider.name != launcherName)
		{
			Debug.Log(collider.name);
			collider.GetComponent<CBehaviour>().GetHit(pColor, attackValue, 2);
			Destroy(this.gameObject);
		}
		else
		{
			Destroy(gameObject);
		}
	}
}
