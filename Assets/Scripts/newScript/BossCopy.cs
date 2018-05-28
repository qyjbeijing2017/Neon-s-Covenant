using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCopy : Boss
{
    Boss boss;

    // Use this for initialization
    void Start()
    {
        characterType = CharacterType.boss;
        boss = FindObjectOfType<Boss>();
        if (boss.skillLast == SkillBoss.tornado)
            animator.Play("Boss_tornado_start");
        else
            animator.Play("boss_range");
    }



    // Update is called once per frame



    /// <summary>
    /// 重写allReady让bosscopy回调；
    /// </summary>
    public override void allReady()
    {
        boss.bossCopyReturn();
        Destroy(gameObject);
    }

    public override bool injured(Attack attack)
    {
        bool injuredBoss = base.injured(attack);

        if (HP < boss.HP)
        {
            boss.HP = HP;
        }
        return injuredBoss;

    }

}
