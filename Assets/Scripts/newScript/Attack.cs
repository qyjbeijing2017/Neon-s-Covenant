using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour {
    [Header("伤害")]
    [Tooltip("造成多少伤害")]
    public int damage;
    [Tooltip("能量伤害、或者对怪物伤害类型")]
    public NSC_Color powerDamage;
    [Tooltip("伤害发出者类型")]
    public NSC_Character.CharacterType m_characterType;
    [Tooltip("造成硬直的时间")]
    public float stopTime;

}
