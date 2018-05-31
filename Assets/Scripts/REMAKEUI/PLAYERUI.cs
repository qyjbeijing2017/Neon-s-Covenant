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
        //稍微处理了一下，不用全部都刷新一遍
        if (hpCache < player.HP)
        {
            for (int i = hpCache; i < player.HP; i++)
            {
                HP[i + 1].enabled = true;
            }
        }
        else if (hpCache == player.HP)
        {

        }
        else
        {
            for (int i = player.HP; i < hpCache; i++)
            {
                HP[i + 1].enabled = false;
            }
        }

        powerWhite.value = player.whitePower.colorValue;
        powerOther.value = player.power.colorValue;

        if (player.power.m_colorType == NSC_Color.colorType.red)
        {
            colorPower.color = red;
        }
        else
            colorPower.color = cyan;

    }

}
