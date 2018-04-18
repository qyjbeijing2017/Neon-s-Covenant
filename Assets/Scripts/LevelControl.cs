using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelControl : MonoBehaviour
{
	public int monsterCount = 0;
	public int bossCount = 0;
	public Vector3 savedBossPosition;

	/// <summary>
	/// Monster及Boss初始化时在关卡管理类中注册该对象
	/// </summary>
	virtual public void MonsterRegister()
	{
		monsterCount += 1;
	}

	/// <summary>
	/// 注销死亡/消失的Monster及Boss
	/// </summary>
	virtual public void MonsterDelete()
	{
		monsterCount -= 1;
	}

	virtual public void BossRegister()
	{
		bossCount += 1;
	}

	virtual public void BossDelete()
	{
		bossCount -= 1;
	}

	/// <summary>
	/// 调用以判断通关条件
	/// </summary>
	virtual public void Judge()
	{

	}
}