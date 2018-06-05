//我想写一个脚本，用来处理人物跌落场景的事件
//本来的设计是触发这个脚本绑定的trigger就控制人物的刚体，使其停止运动，但不知怎么就做不到呢~

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillY : MonoBehaviour
{

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "MainCharacter")
        {
			//Kill Game
			other.GetComponent<Rigidbody>().useGravity = false;
			other.GetComponent<Rigidbody>().velocity = Vector3.zero;
			Debug.Log("QaQ");
        }
    }
}
