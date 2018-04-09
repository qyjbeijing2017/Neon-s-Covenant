using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAnim : CAnim
{
	void Awake()
	{
		base.Awake();
	}

	// Update is called once per frame
	void Update()
	{
		SetMovingAnim((behaviour as BossBehaviour).IsChasing());
	}

	//动画的回调函数
	public void AnimStopDecision()
	{
		(behaviour as BossBehaviour).StopCoroutine("DecisionCR");
	}

	public void AnimStartDecision()
	{
		(behaviour as BossBehaviour).StartCoroutine("DecisionCR");
	}

	public void CheckIfSeparated()
	{
		if (property.level.bossCount != 1)
		{
			if ((property as BossProperty).isFake)
			{
				Destroy(this.gameObject);
			}
			else
				transform.position = property.level.savedBossPosition;
		}
	}
}
