using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldDisplayer : MonoBehaviour
{
	public BossProperty boss;
	UnityEngine.UI.Text text;
	// Update is called once per frame
	void Update()
	{
		text = GetComponent<UnityEngine.UI.Text>();
		text.text = "Shield: " + boss.shield + ", Shield Color: " + boss.shieldColor;
	}
}
