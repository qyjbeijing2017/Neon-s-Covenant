//用来处理圆形双层UI的简单脚本

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PLAYERUI : MonoBehaviour
{
    [SerializeField] Player player;
    [SerializeField] Slider powerWhite;
    [SerializeField] Slider powerOther;
    [SerializeField] Color cyan = Color.cyan;
    [SerializeField] Color red = Color.red;
    [SerializeField] Image[] HP;
    [SerializeField] Image colorPower;

    int hpCache = 0;
    float pMax = 0;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("MainCharacter").GetComponent<Player>();
    }
    
    void Start()
    {
        hpCache = player.HP;

        for (int i = 0; i < HP.Length; i++)
        {
            HP[i].enabled = false;
        }
        for (int i = 0; i < player.HP; i++)
        {
            HP[i].enabled = true;
        }

        //修改血条上限
        pMax = player.powerMax;
        powerOther.maxValue = pMax;
        powerWhite.maxValue = pMax;

        //这个地方的max是白色吗？
        powerOther.value = player.power.colorValue;
        powerWhite.value = player.whitePower.colorValue;

        if (player.power.m_colorType == NSC_Color.colorType.red)
        {
            colorPower.color = red;
        }
        else
            colorPower.color = cyan;
    }

    void Update()
    {
        if (hpCache != player.HP)
        {
            for (int i = 0; i < HP.Length; i++)
            {
                HP[i].enabled = false;
            }
            for (int i = 0; i < player.HP; i++)
            {
                HP[i].enabled = true;
            }
        }

        powerWhite.value = player.whitePower.colorValue;  //白色血条值是目前总能量值
        powerOther.value = player.power.colorValue + player.whitePower.colorValue;

        if (player.power.m_colorType == NSC_Color.colorType.red)
        {
            colorPower.color = red;
        }
        else
            colorPower.color = cyan;

    }

}
