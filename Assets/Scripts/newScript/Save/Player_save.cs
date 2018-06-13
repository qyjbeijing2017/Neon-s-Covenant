using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_save : MonoBehaviour
{
    [SerializeField] Player player;

    Vector3 playerPosition;
    int playerHP;
    NSC_Color playerOtherPower;
    float whitePower;

    [SerializeField] Monster[] monsters;
    List<Vector3> monstersSave = new List<Vector3>();



    // Use this for initialization
    void Start()
    {
        playerOtherPower = new NSC_Color();
        monsters = FindObjectsOfType<Monster>();
        player = FindObjectOfType<Player>();
        for (int i = 0; i < monsters.Length; i++)
        {
            monstersSave.Add(monsters[i].transform.position);
        }
        SavePlayer();

    }

    // Update is called once per frame
    void Update()
    {

    }


    public void ReadMonster()
    {
        for (int i = 0; i < monsters.Length; i++)
        {
            if (!monsters[i].dead)
            {
                monsters[i].transform.position = monstersSave[i];
                monsters[i].HP = monsters[i].HPMax;
                monsters[i].allReady();
                monsters[i].animator.Play("Idle");
            }

        }
    }

    /// <summary>
    /// 保存player
    /// </summary>
    public void SavePlayer()
    {
        playerPosition = player.transform.position;
        playerHP = player.HP;
        playerOtherPower.colorValue = player.power.colorValue;
        playerOtherPower.m_colorType = player.power.m_colorType;
        whitePower = player.whitePower.colorValue;

    }

    public void SaveMonster()
    {

    }
    /// <summary>
    /// 读取player
    /// </summary>
    /// <returns></returns>
    public void ReadPlayer()
    {
        player.transform.position = playerPosition;
        player.HP = playerHP;
        player.power.colorValue = playerOtherPower.colorValue;
        player.power.m_colorType = playerOtherPower.m_colorType;
        player.whitePower.colorValue = whitePower;
    }

    public void ReadGame()
    {
        ReadPlayer();
        ReadMonster();
    }

}
