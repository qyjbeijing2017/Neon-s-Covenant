using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Player_UI : MonoBehaviour {
    [SerializeField] Player_new player;
    [SerializeField] Slider powerWhite;
    [SerializeField] Slider powerOther;
    [SerializeField] Color cyan;
    [SerializeField] Color red;
    [SerializeField] Image[] HP;
    [SerializeField] Image colorPower;

	// Use this for initialization
	void Start () {
        for(int i = 0; i < HP.Length; i++)
        {
            HP[i].enabled = false;
        }
        for (int i = 0; i < player.HP; i++)
        {
            HP[i].enabled = true;
        }
        powerWhite.value = player.power / player.powerMax;
        powerOther.value = (player.power + player.powerColor) / player.powerMax;
        if (player.powerType == 1)
            colorPower.color = red;
        if(player.powerType == 2)
            colorPower.color = cyan;



    }
	
	// Update is called once per frame
	void Update () {
        for (int i = 0; i < HP.Length; i++)
        {
            HP[i].enabled = false;
        }
        for (int i = 0; i < player.HP; i++)
        {
            HP[i].enabled = true;
        }
        powerWhite.value = player.power / player.powerMax;
        powerOther.value = (player.power + player.powerColor) / player.powerMax;
        if (player.powerType == 1)
            colorPower.color = red;
        if (player.powerType == 2)
            colorPower.color = cyan;
    }
}
