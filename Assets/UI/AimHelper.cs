using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimHelper : MonoBehaviour
{
	LineRenderer aimLine;
	//这个类的作用在于右键点击地面时显示一个辅助瞄准器
	public void Draw(Vector3 ori, Vector3 offset)
	{
		if (aimLine == null)
		{
			if (aimLine = GetComponent<LineRenderer>()) { aimLine.SetPosition(0, ori); aimLine.SetPosition(1, offset); }
			else
			{
				print("Can't found the lineRender.");
			}
		}
		else
		{
			aimLine.SetPosition(0, ori);
			aimLine.SetPosition(1, offset);
		}
	}
}
