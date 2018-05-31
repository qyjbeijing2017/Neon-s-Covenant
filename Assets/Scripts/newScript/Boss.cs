using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : NSC_Character
{
    public enum SkillBoss
    {
        nearAttack = 0,
        rangeAttack = 1,
        tornado = 2

    }

    [Header("boss特权")]
    [Tooltip("boss二阶段血量")]
    [SerializeField] float mode2HP;
    [Tooltip("近身闪现位置")]
    [SerializeField] float nearFlashDis;
    [Tooltip("远程攻击数量")]
    [SerializeField] float nubRangeAttack;
    [Tooltip("远程增益角")]
    [SerializeField] float angleRange;
    [Tooltip("复制boss")]
    [SerializeField] BossCopy bossCopy;
    [Tooltip("bossf复制体间隔距离")]
    [SerializeField] float flashDisBoss2;
    [Tooltip("旋风红")]
    [SerializeField] Tornado tornadoRed;
    [Tooltip("旋风青")]
    [SerializeField] Tornado tornadoCyan;
    Player player;
    [HideInInspector] bool m_allReady;
    [HideInInspector] int bossCopyNub;
    [HideInInspector] Vector3 HidePosition;

    [HideInInspector] public SkillBoss skillLast;

    // Use this for initialization
    void Start()
    {
        characterType = CharacterType.boss;
        player = FindObjectOfType<Player>();
        animator.StopPlayback();
        skillLast = SkillBoss.tornado;
    }

    public override bool injured(Attack attack)
    {
        return base.injured(attack);
    }

    virtual public void tornadoShoot()
    {
        if (power.m_colorType == NSC_Color.colorType.red)
            Instantiate(tornadoRed.gameObject, shootPoint.position, shootPoint.rotation).GetComponent<Tornado>().attack.powerDamage.m_colorType = power.m_colorType;
        else if (power.m_colorType == NSC_Color.colorType.cyan)
            Instantiate(tornadoCyan.gameObject, shootPoint.position, shootPoint.rotation).GetComponent<Tornado>().attack.powerDamage.m_colorType = power.m_colorType;
    }
    /// <summary>
    /// boss近战碰撞盒重写数组0为第一个攻击的手，数组1为第二个攻击的手，数组2为双手。
    /// </summary>
    /// <param name="attackNub"></param>
    public override void startNearAttack(int attackNub)
    {

        switch (attackNub)
        {
            case 0: changeNearAttack(0, 0); break;
            case 1: changeNearAttack(1, 0); break;
            case 2: changeNearAttack(0, 0); changeNearAttack(1, 0); break;
        }

    }


    void changeNearAttack(int attackCollider, int attackNub)
    {
        stopNearAttack();
        Attack attack = nearAttackCollider[attackCollider];
        attack.damage = nearAttack[attackNub].damage;
        attack.powerDamage = nearAttack[attackNub].powerDamage;
        attack.powerDamage.m_colorType = power.m_colorType;
        attack.m_characterType = characterType;
        attack.stopTime = nearAttack[attackNub].stopTime;
        nearAttackCollider[attackCollider].GetComponent<Collider>().enabled = true;
    }
    /// <summary>
    /// boss隐藏
    /// </summary>
    /// <param name="a"></param>
    public void bossHide(int a)
    {
        HidePosition = transform.position;
        transform.position = new Vector3(0, 0, -1000);
        animator.speed = 0;
        GameObject boss1 = Instantiate(bossCopy.gameObject);
        boss1.transform.position = player.transform.position + (player.transform.right * flashDisBoss2);
        boss1.transform.forward = player.transform.position - boss1.transform.position;
        GameObject boss2 = Instantiate(bossCopy.gameObject);
        boss2.transform.position = player.transform.position - (player.transform.right * flashDisBoss2);
        boss2.transform.forward = player.transform.position - boss2.transform.position;
        if (a == 1)
        {
            boss1.GetComponent<BossCopy>().animator.SetBool("rangeAttack", true);
            boss2.GetComponent<BossCopy>().animator.SetBool("rangeAttack", true);
        }
        else
        {
            boss1.GetComponent<BossCopy>().animator.SetBool("tornado", true);
            boss1.GetComponent<BossCopy>().power.m_colorType = NSC_Color.colorType.cyan;
            boss2.GetComponent<BossCopy>().animator.SetBool("tornado", true);
            boss2.GetComponent<BossCopy>().power.m_colorType = NSC_Color.colorType.red;
        }

    }
    /// <summary>
    /// boss复制体回调
    /// </summary>
    public void bossCopyReturn()
    {
        bossCopyNub++;
        if (bossCopyNub >= 2)
        {
            this.transform.position = HidePosition;
            animator.speed = 1;
            if (HP < 0)
            {
                animator.Play("die");
            }
            else
            {
                animator.Play("Idle");
            }

        }
    }
    /// <summary>
    /// 虚弱
    /// </summary>
    public void weak()
    {
        animator.Play("weak");
    }
    /// <summary>
    /// 可以做下次决策。
    /// </summary>
    public virtual void allReady()
    {
        if (!animator.IsInTransition(0))
        {
            m_allReady = true;
            power.m_colorType = NSC_Color.colorType.white;
            animator.SetBool("tornado", false);
            animator.SetBool("weak", false);
            animator.SetBool("nearAttack", false);
            animator.SetBool("rangeAttack", false);

        }


    }
    /// <summary>
    /// 决策
    /// </summary>
    public void detectRandom()
    {
        int a = Random.Range(0, 3);
        while (a == skillLast.GetHashCode())
        {
            a = Random.Range(0, 3);
        }
        if (mode2HP >= HP)
        {

            switch (a)
            {
                case 0: animator.SetBool("nearAttack", true); skillLast = SkillBoss.nearAttack; break;
                case 1: skillLast = SkillBoss.rangeAttack; bossHide(1); break;
                case 2: skillLast = SkillBoss.tornado; bossHide(2); break;
            }
        }
        else
        {
            switch (a)
            {
                case 0: animator.SetBool("nearAttack", true); skillLast = SkillBoss.nearAttack; break;
                case 1: animator.SetBool("rangeAttack", true); skillLast = SkillBoss.rangeAttack; break;
                case 2: animator.SetBool("tornado", true); skillLast = SkillBoss.tornado; break;
            }
        }
    }
    /// <summary>
    /// boss射击
    /// </summary>
    /// <param name="typeRange"></param>
    override public void shoot(int typeRange)
    {
        for (int i = 0; i < nubRangeAttack; i++)
        {
            transform.localEulerAngles += new Vector3(0, angleRange * typeRange, 0);
            float M_angle = 360 / nubRangeAttack;
            if (typeRange == 1)
                Instantiate(attackRed.gameObject, shootPoint.position, shootPoint.rotation).GetComponent<Rigidbody>().velocity = shootPoint.forward * 6;
            else if (typeRange == 2)
                Instantiate(attackCyan.gameObject, shootPoint.position, shootPoint.rotation).GetComponent<Rigidbody>().velocity = shootPoint.forward * 6;
            else if (typeRange == 3)
                Instantiate(attackBlack.gameObject, shootPoint.position, shootPoint.rotation).GetComponent<Rigidbody>().velocity = shootPoint.forward * 6;
            transform.localEulerAngles += new Vector3(0, M_angle, 0);
            transform.localEulerAngles -= new Vector3(0, angleRange * typeRange, 0);
        }

    }
    /// <summary>
    /// 闪现copy
    /// </summary>
    public void nearAttack_flash()
    {
        Vector3 positionNew = player.transform.position + (transform.position - player.transform.position).normalized * nearFlashDis;
        transform.position = new Vector3(positionNew.x, transform.position.y, positionNew.z);
        transform.forward = new Vector3(player.transform.position.x - transform.position.x, 0, player.transform.position.z - transform.position.z);
        //       Debug.Break();
    }
    /// <summary>
    /// 随机改变颜色
    /// </summary>
    public virtual void changeColorRandom()
    {
        if (Random.Range(0, 2) < 1)
        {
            power.m_colorType = NSC_Color.colorType.red;
        }
        else
        {
            power.m_colorType = NSC_Color.colorType.cyan;
        }
    }

    /// <summary>
    /// 变回白色
    /// </summary>
    /// <param name="i_colorType"></param>
    public virtual void changeColor(int i_colorType)
    {
        power.m_colorType = NSC_Color.colorType.white;
    }
    
}
