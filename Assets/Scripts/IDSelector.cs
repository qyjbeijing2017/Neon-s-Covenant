using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IDSelector : MonoBehaviour
{
	//这个脚本是用来解决GetComponent<T>中T填充问题的

	Component idComponent;

	public enum IdentityOfObject
	{
		MainCharacter,
		Monster,
		Boss,
		Object
	};

	// Use this for initialization
	void Start()
	{
		MainCharacterBehaviour a = (MainCharacterBehaviour)new Component();
	}
}
