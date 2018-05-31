using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class NSC_Character : MonoBehaviour
{
    public enum CharacterType
    {
        player = 0,
        boss = 1,
        enemy = 2
    }
    [Header("生物基础")]
    [Tooltip("生物血量")]
    public int HP;
    [Tooltip("血量上限")]
    public int HPMax;
    [Tooltip("主角的异常能量，或者怪物当前的颜色类型")]
    public NSC_Color power;
    [Tooltip("能量槽上限")]
    public float powerMax;
    [Tooltip("角色类型，用于判定伤害以及其他")]
    public CharacterType characterType;
    [Tooltip("角色的近战伤害.可以进行多段数赋值。同时开启2个attack时硬直时间不相加伤害相加")]
    public Attack[] nearAttack;
    [Tooltip("角色近战碰撞，一般只有一个，boss的左酱和右酱会在代码里面重写")]
    public Attack[] nearAttackCollider;
    [Space(20)]
    [Header("远程相关")]
    [Tooltip("角色远程伤害,白色")]
    public AttackRange attackWhite;
    [Tooltip("角色远程伤害,红色")]
    public AttackRange attackRed;
    [Tooltip("角色远程伤害,青色")]
    public AttackRange attackCyan;
    [Tooltip("角色远程伤害,黑色")]
    public AttackRange attackBlack;
    [Tooltip("角色远程射击点")]
    public Transform shootPoint;


    [Space(20)]
    [Header("伤害相关")]
    [Tooltip("反色伤害加深,都为：伤害*damageIN")]
    public float damageIn;
    [Tooltip("同色伤害削减，都为：伤害*damageDe")]
    public float damageDe;


    [Space(20)]
    [Header("死亡相关")]
    [Tooltip("角色死亡动画放完后删除时间")]
    float deadDelay;
    [Tooltip("角色是否死亡")]
    public bool dead;

    [HideInInspector]public Animator animator;
    protected void Awake()
    {
        GetComponent<Rigidbody>().isKinematic = true;
        animator = GetComponent<Animator>();
        for (int i = 0; i < nearAttackCollider.Length; i++)
        {
            if (nearAttackCollider[i].GetComponent<Collider>())
            {
                nearAttackCollider[i].GetComponent<Collider>().isTrigger = true;
            }
            else
            {


                Debug.LogError("武器丢失碰撞盒");
                Debug.Break();
            }
        }
        stopNearAttack();
    }

    /// <summary>
    /// 角色受伤,这里只是写了boss跟小怪的规则，player受伤机制特殊。
    /// </summary>
    /// <param name="attack"></param>
    /// <returns></returns>
    virtual public bool injured(Attack attack)
    {
        if (characterType == CharacterType.boss || characterType == CharacterType.enemy)
        {
            if (attack.m_characterType == CharacterType.player)
            {
                if (NSC_Color.colorSame(attack.powerDamage, power) && attack.powerDamage.m_colorType != NSC_Color.colorType.white)
                {
                    HP -= (int)(attack.damage * damageDe);

                }
                else if (NSC_Color.colorContrary(attack.powerDamage, power))
                {
                    HP -= (int)(attack.damage * damageIn);
                }
                else
                {
                    HP -= attack.damage;
                }
                HPNormal();
                if (!dead && characterType != CharacterType.boss && attack.stopTime > 0.0f)
                {
                    StopCoroutine("characterStop");
                    StartCoroutine("characterStop", attack);
                }
                return true;
            }
        }

        return false;
    }
    /// <summary>
    /// 用于规整HP和Power的不正常状态，比如HP小于零。
    /// </summary>
    virtual public void HPNormal()
    {
        if (HP <= 0)
        {
            dead = true;
            HP = 0;
            animator.speed = 1;
            animator.Play("die");
        }
        if (power.colorValue <= 0)
        {
            power.colorValue = 0;
        }
        if (power.colorValue >= powerMax)
        {
            power.colorValue = powerMax;
        }
        if (HP > HPMax)
        {
            HP = HPMax;
        }
        if (power.colorValue > powerMax)
        {
            power.colorValue = powerMax;
        }

    }
    /// <summary>
    /// 任何角色的硬直。
    /// </summary>
    /// <param name="other"></param>
    virtual public IEnumerator characterStop(Attack attack)
    {
        if (characterType != CharacterType.boss)
        {
            animator.Play("injured");
            stopNearAttack();
            animator.SetBool("stop", true);
            yield return new WaitForSeconds(attack.stopTime);
            animator.SetBool("stop", false);
        }
    }
    /// <summary>
    /// 近战伤害开启，只有第一个碰撞盒有效，boss的左酱右酱需要重写。int值表示这次伤害的位于伤害数组的第几位（从0开始）
    /// </summary>
    /// <param name="other"></param>
    virtual public void startNearAttack(int attackNub)
    {
        stopNearAttack();
        Attack attack = nearAttackCollider[0];
        attack.damage = nearAttack[attackNub].damage;
        attack.powerDamage = nearAttack[attackNub].powerDamage;
        attack.powerDamage.m_colorType = power.m_colorType;
        attack.m_characterType = characterType;
        attack.stopTime = nearAttack[attackNub].stopTime;
        nearAttackCollider[0].GetComponent<Collider>().enabled = true;
    }
    /// <summary>
    /// 近战伤害关闭。
    /// </summary>
    virtual public void stopNearAttack()
    {
        for (int i = 0; i < nearAttackCollider.Length; i++)
        {
            nearAttackCollider[i].GetComponent<Collider>().enabled = false;
        }


    }
    /// <summary>
    /// 远程射击。boss的弹幕需要重写。
    /// </summary>
    virtual public void shoot(int typeRange)
    {

        if (power.m_colorType == NSC_Color.colorType.white)
        {
            Instantiate(attackWhite.gameObject, shootPoint.position, shootPoint.rotation).GetComponent<Rigidbody>().velocity = transform.forward;
        }
        else if (power.m_colorType == NSC_Color.colorType.black)
        {
            Instantiate(attackBlack.gameObject, shootPoint.position, shootPoint.rotation).GetComponent<Rigidbody>().velocity = transform.forward;
        }
        else if (power.m_colorType == NSC_Color.colorType.red)
        {
            Instantiate(attackRed.gameObject, shootPoint.position, shootPoint.rotation).GetComponent<Rigidbody>().velocity = transform.forward;
        }
        else if (power.m_colorType == NSC_Color.colorType.cyan)
        {
            Instantiate(attackCyan.gameObject, shootPoint.position, shootPoint.rotation).GetComponent<Rigidbody>().velocity = transform.forward;
        }

    }

    /// <summary>
    /// 死亡结束销毁。
    /// </summary>
    /// <param name="other"></param>
    virtual public void deadDestory()
    {
        Destroy(gameObject, deadDelay);
    }

    protected void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Attack>() && other.GetComponent<Attack>().m_characterType != characterType)
        {
           
            if (injured(other.GetComponent<Attack>()) && other.GetComponent<AttackRange>())
                Destroy(other.gameObject);
        }
    }

}
