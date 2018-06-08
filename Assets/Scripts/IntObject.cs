using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntObject : MonoBehaviour
{

	[SerializeField] GameObject notification;   //用来提示
	[SerializeField] UnityEngine.UI.Text text;
	[SerializeField] string content;
	enum TypeOfGizmo { 告示牌, 机关, 其他 };
	[SerializeField] TypeOfGizmo 物体类型;
    private Player_new p;
    bool isStopped=false;
	bool trigger;

	bool mCharIn = false;   //主角是否在控制范围中

	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "MainCharacter")
		{
			mCharIn = true;
			Display();
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (other.tag == "MainCharacter")
		{
			mCharIn = false;
			Display();
		}
	}

	void Display() //显示\隐藏提示板
	{
		if (mCharIn)
		{
			text.text = content;
		}
		else
		{
			text.text = "";
		}
	}

	void Start()
	{

	}

	private void Update()
	{
		if (mCharIn && Input.GetKeyDown(KeyCode.E))
		{
			switch (物体类型)
			{
				case TypeOfGizmo.告示牌: { CanvasEffect.Diffuse(); CanvasEffect.DisplayNotice();
                        if (!isStopped)
                        {
                            print(2);
                            FindObjectOfType<Player_new>().player_stopImmediately();
                            isStopped = true;
                        }
                        else
                        {
                            print(1);
                            FindObjectOfType<Player_new>().player_start();
                            isStopped = false;
                        }

                    } break;
				case TypeOfGizmo.机关:; break;
				case TypeOfGizmo.其他:; break;
			}
		}
	}
}