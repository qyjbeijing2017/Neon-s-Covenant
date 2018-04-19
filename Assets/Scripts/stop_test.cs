using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stop_test : MonoBehaviour {
    enum HurtType
    {
        Boss,
        player,
        enemy
    }
    enum DamageType
    {
        white = 0,
        red = 1,
        cyan = 2,
        black = 3
    }
    [SerializeField] HurtType hurtEnemy;
    [SerializeField] KeyCode hurtkey;
    [SerializeField] KeyCode boss_laser;
    [SerializeField] KeyCode boss_nearAttack;
    [SerializeField] KeyCode boss_rangeAttack;
    [SerializeField] KeyCode boss_mode2;
    [SerializeField] KeyCode boss_weak;
    [SerializeField] KeyCode enemy_attack;
    [SerializeField] float stopTime;
    [SerializeField] DamageType damageType;
    [SerializeField] float powerDamage;
    [SerializeField] int damage;
    Boss_new[] boss;
    Enemy[] enemy;
    Player_new player;

	// Use this for initialization
	void Start () {
        player = FindObjectOfType<Player_new>();
        enemy = FindObjectsOfType<Enemy>();
        boss = FindObjectsOfType<Boss_new>();

    }
	
	// Update is called once per frame
	void Update () {
        if(hurtEnemy == HurtType.Boss)
        {
            if (Input.GetKeyDown(hurtkey))
            {
                 for(int i = 0; i < boss.Length; i++)
                {
                    boss[i].injured(damage, damage.GetHashCode());
                }
            }

            if (Input.GetKeyDown(boss_laser))
            {
                for (int i = 0; i < boss.Length; i++)
                {
                    boss[i].test_bossLaser();
                }
            }

            if (Input.GetKeyDown(boss_nearAttack))
            {
                for (int i = 0; i < boss.Length; i++)
                {
                    boss[i].test_bossNearAttack();
                }
            }

            if (Input.GetKeyDown(boss_rangeAttack))
            {
                for (int i = 0; i < boss.Length; i++)
                {
                    boss[i].test_bossRangeAttack();
                }
            }

            if (Input.GetKeyDown(boss_mode2))
            {
                for (int i = 0; i < boss.Length; i++)
                {
                    boss[i].test_bossMode2();
                }
            }
            if (Input.GetKeyDown(boss_weak))
            {
                for (int i = 0; i < boss.Length; i++)
                {
                    boss[i].test_bossWeak();
                }
            }
        }
        
        if(hurtEnemy == HurtType.player)
        {
            if(Input.GetKeyDown(hurtkey))
            player.inJured(damage, powerDamage, damageType.GetHashCode(), stopTime);
        }
        if (hurtEnemy == HurtType.enemy)
        {
            if (Input.GetKeyDown(hurtkey))
            {
                for (int i = 0; i < boss.Length; i++)
                {
                    enemy[i].injured(damage, damage.GetHashCode(), stopTime);
                }
            }
        }

        if (Input.GetKeyDown("o"))
        {
            int a = damageType.GetHashCode();
            print(a);
        }
    }
}
