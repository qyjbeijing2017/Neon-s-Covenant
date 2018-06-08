using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddHp : MonoBehaviour {
    Player player;
    [Tooltip("回血键"),SerializeField]KeyCode key;
    [Tooltip("消耗白光量"), SerializeField] float needPower;
    // Use this for initialization
    void Start () {
        player = GetComponent<Player>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(key))
        {
            
            if (player.move)
            {
                print(1);
                if (player.HPMax >player.HP && player.whitePower.colorValue > needPower)
                {
                    player.HP += 1;
                    player.whitePower.colorValue -= needPower;
                }
            }
        }
	}
}
