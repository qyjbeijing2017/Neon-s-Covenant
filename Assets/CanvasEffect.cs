using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasEffect : MonoBehaviour
{

	[SerializeField] GameObject maoboli;
	static GameObject 毛玻璃;
	[SerializeField] GameObject 死普莱特;
	static GameObject Sprite;
	static bool shown = false;

	private void Start()
	{
		毛玻璃 = maoboli;
		Sprite = 死普莱特;
	}
	public static void Diffuse()
	{
		if (!CanvasEffect.shown)
			CanvasEffect.毛玻璃.SetActive(true);
		else
			CanvasEffect.毛玻璃.SetActive(false);
	}

	public static void DisplayNotice()
	{
		if (!CanvasEffect.shown)
		{
			CanvasEffect.Sprite.SetActive(true);
			shown = true;
		}

		else
		{
			CanvasEffect.Sprite.SetActive(false);
			shown = false;
		}
	}

}
