using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// CProperty类依赖Rigidbody
/// </summary>
[RequireComponent(typeof(Rigidbody))]
public class CProperty : MonoBehaviour
{
	public LevelControl level;
	public string typeOfObject;
	public bool interactable;
	[SerializeField] public int mainColor;
	[SerializeField] public float mainColorValue;
	[SerializeField] public int otherColor =0;
	[SerializeField] public float otherColorValue;
	[SerializeField] protected bool isAlive = true;

	protected CBehaviour behaviour;
	protected CAnim anim;

	protected void Awake()
	{
        level = FindObjectOfType<LevelControl>();

		behaviour = GetComponent<CBehaviour>();
		anim = GetComponent<CAnim>();
	}

	protected void Update()
	{
		if (!isAlive)
			behaviour.Die();

		//这里预留一个位置 用来处理UI
	}
}
