using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestB : TestA
{
	/// <summary>
	/// something like this.
	/// </summary>
	public float something;
	// Use this for initialization
	protected override void Start()
	{
		base.Start();
		Debug.Log("You");
	}
}
