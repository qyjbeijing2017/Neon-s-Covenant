using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NSC_Character : MonoBehaviour
{
    public enum CharacterType
    {
        player = 0,
        boss = 1,
        enemy = 2
    }

    [Tooltip("生物血量")]
    public int HP;
    [Tooltip("主角的能量，或者怪物当前的颜色类型")]
    public NSC_Color power;
    [Tooltip("角色类型，用于判定伤害以及其他")]
    public CharacterType characterType;
    [Tooltip("角色的近战伤害")]
    public Attack nearAttack;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
