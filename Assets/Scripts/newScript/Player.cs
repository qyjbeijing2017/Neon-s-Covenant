using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(CharacterController))]
public class Player : NSC_Character
{
    [Space(20)]
    [Header("角色移动")]
    [Tooltip("移动速度")]
    public float speed;
    [Tooltip("翻滚速度")]
    public float rollingSpeed;
    [Tooltip("翻滚CD")]
    public float rollCD;
    [Tooltip("攻击移动")]
    public float m_nearattackMove;
    [Header("特殊状态")]
    [Tooltip("白颜色槽")]
    public NSC_Color whitePower;
    [Tooltip("远程消耗")]
    public Attack rangeAttackPower;
    [Header("需求组件")]
    [Tooltip("射击准线")]
    [SerializeField] private SpriteRenderer aimLine;
    [Tooltip("鼠标射线物理层")]
    [SerializeField] int layerMouse;

    CharacterController characterController;
    [HideInInspector]public bool move;//角色是否可移动
    bool rollingCD;
    bool comboNow;
    [SerializeField]bool weakBoss;

    // Use this for initialization
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        move = true;
        rollingCD = false;
        aimLine.enabled = false;
        comboNow = false;
        weakBoss = false;
    }

    public override void shoot(int typeRange)
    {
        base.shoot(typeRange);
        injured(rangeAttackPower);


    }
    /// <summary>
    /// 角色受伤重写
    /// </summary>
    /// <param name="attack"></param>
    /// <returns></returns>
    public override bool injured(Attack attack)
    {
        if (weakBoss && NSC_Color.colorContrary(power, attack.powerDamage) && FindObjectOfType<Boss>().animator.GetBool("nearAttack"))
        {
            FindObjectOfType<Boss>().weak();
            return false;
        }
        if (!animator.GetBool("rolling"))
        {
            animatorEnd();
            HP -= attack.damage;
            powerInjured(attack);
            HPNormal();
            if (!dead && characterType != CharacterType.boss && attack.stopTime > 0.0f)
            {
                move = false;
                StopCoroutine("characterStop");
                StartCoroutine("characterStop", attack);
            }
            return true;
        }
        return false;
    }
    /// <summary>
    /// 角色能量遭到伤害。
    /// </summary>
    /// <param name="attack"></param>
    void powerInjured(Attack attack)
    {
        if (NSC_Color.colorSame(attack.powerDamage, power) && power.m_colorType != NSC_Color.colorType.white)
        {
            if (attack.powerDamage.colorValue > whitePower.colorValue)
            {
                power.colorValue += whitePower.colorValue;
                whitePower.colorValue = 0;
            }
            else
            {
                power.colorValue += attack.powerDamage.colorValue;
                whitePower.colorValue -= attack.powerDamage.colorValue;
            }
        }
        else if (NSC_Color.colorContrary(attack.powerDamage, power))
        {
            if (attack.powerDamage.colorValue < power.colorValue)
            {
                power.colorValue -= attack.powerDamage.colorValue;
                whitePower.colorValue += attack.powerDamage.colorValue;
            }
            else
            {
                whitePower.colorValue += power.colorValue;
                power.colorValue = attack.powerDamage.colorValue - power.colorValue;
                power.m_colorType = attack.powerDamage.m_colorType;
                if (power.colorValue > whitePower.colorValue)
                {
                    whitePower.colorValue = 0;
                    power.colorValue = whitePower.colorValue;
                }
                else
                {
                    whitePower.colorValue -= power.colorValue;
                }
            }
        }
        else if (attack.powerDamage.m_colorType == NSC_Color.colorType.black)
        {
            if (attack.powerDamage.colorValue >= power.colorValue)
            {

                if (whitePower.colorValue > attack.powerDamage.colorValue - power.colorValue)
                {
                    whitePower.colorValue -= attack.powerDamage.colorValue - power.colorValue;
                }
                else
                {
                    whitePower.colorValue = 0;
                }
                power.colorValue = 0;
                power.m_colorType = NSC_Color.colorType.white;
            }
            else
            {
                power.colorValue -= attack.powerDamage.colorValue;
            }
        }
        else if (attack.powerDamage.m_colorType == NSC_Color.colorType.white)
        {
            whitePower.colorValue += attack.powerDamage.colorValue;
        }
        else if (power.m_colorType == NSC_Color.colorType.white)
        {
            whitePower.colorValue -= attack.powerDamage.colorValue;
            power.colorValue += attack.powerDamage.colorValue;
            power.m_colorType = attack.powerDamage.m_colorType;
        }
    }
    /// <summary>
    /// 重写HPNormal
    /// </summary>
    public override void HPNormal()
    {
        base.HPNormal();
        if (whitePower.colorValue <= 0)
        {
            whitePower.colorValue = 0;
            whitePower.m_colorType = NSC_Color.colorType.white;
        }
        if (whitePower.colorValue >= powerMax)
        {
            whitePower.colorValue = powerMax;
        }
        if (power.colorValue + whitePower.colorValue > powerMax)
        {
            whitePower.colorValue += powerMax - power.colorValue - whitePower.colorValue;
        }
        if (power.colorValue <= 0)
        {
            power.m_colorType = NSC_Color.colorType.white;
        }
    }

    /// <summary>
    /// 连击启动
    /// </summary>
    public void comboStart()
    {
        animator.SetBool("combo", false);
        comboNow = true;
    }

    /// <summary>
    /// 连击结束
    /// </summary>
    public void comboEnd()
    {
        animator.SetBool("combo", false);
        comboNow = false;
        animatorEnd();
    }

    /// <summary>
    /// 连击移动
    /// </summary>
    public void nearAttackMove()
    {
        characterController.Move(transform.forward * m_nearattackMove);
    }

    /// <summary>
    /// 结束所有动作
    /// </summary>
    public void animatorEnd()
    {
        animator.SetBool("rolling", false);
        animator.SetBool("nearAttack", false);
        animator.SetBool("rangeAttack", false);
        animator.SetBool("combo", false);
        endWeakBoss();

    }

    /// <summary>
    /// 开启瞄准线
    /// </summary>
    void aimLineStart()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        LayerMask mouseMask = 1 << layerMouse;
        if (Physics.Raycast(ray, out hit, 1000f, mouseMask))
        {
            Vector3 offset = new Vector3((hit.point - transform.position).x, 0, (hit.point - transform.position).z);
            if (offset.magnitude > 0.7)
            {
                transform.forward = new Vector3(offset.x, transform.forward.y, offset.z).normalized;
            }
            aimLine.size = new Vector2((hit.point - this.transform.position).magnitude, aimLine.size.y);
            aimLine.enabled = true;
        }
    }

    /// <summary>
    /// 翻滚结束Event。
    /// </summary>
    public void rollingEnd()
    {
        animator.SetBool("rolling", false);
        animator.SetBool("moving", false);
        rollingCD = true;
        StartCoroutine("rollingCDNow");
    }

    /// <summary>
    /// 翻滚CD。
    /// </summary>
    /// <returns></returns>
    IEnumerator rollingCDNow()
    {
        yield return new WaitForSeconds(rollCD);
        rollingCD = false;
    }

    /// <summary>
    /// 所有状态Idle状态空闲
    /// </summary>
    public void allReady()
    {
        if (!animator.IsInTransition(0))
        {
            move = true;
            animator.SetBool("rolling", false);
            animator.SetBool("nearAttack", false);
            animator.SetBool("rangeAttack", false);
            animator.SetBool("combo", false);

        }


    }

    /// <summary>
    /// 翻滚位移
    /// </summary>
    void rolling()
    {
        
        characterController.SimpleMove(transform.forward * rollingSpeed);
    }

    /// <summary>
    /// 移动CharacterController化。
    /// </summary>
    void moving()
    {
        Vector3 moveV3 = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")) * speed;
        if (moveV3 != Vector3.zero)
        {
            transform.forward = moveV3;
            characterController.SimpleMove(moveV3);
            animator.SetBool("moving", true);
            animator.Play("Running");
        }
        else
        {
            animator.SetBool("moving", false);
            characterController.SimpleMove(moveV3);
            animator.Play("Idle");
        }

    }

    /// <summary>
    /// 虚弱boss
    /// </summary>
    public void startWeakBoss()
    {
        weakBoss = true;
    }

    /// <summary>
    /// 虚弱boss结束；
    /// </summary>
    public void endWeakBoss()
    {
        weakBoss = false;
    }

    /// <summary>
    /// 改变人物方向
    /// </summary>
    public void changeForward()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        LayerMask mouseMask = 1 << layerMouse;
        if (Physics.Raycast(ray, out hit, 100f, mouseMask))
        {
            Vector3 offset = new Vector3((hit.point - transform.position).x, 0, (hit.point - transform.position).z);
            if (offset.magnitude > 0.7)
            {
                transform.forward = new Vector3(offset.x, transform.forward.y, offset.z).normalized;
            }
            aimLine.size = new Vector2((hit.point - this.transform.position).magnitude, aimLine.size.y);
        }
    }

    private void FixedUpdate()
    {
        if (move)
        {
            moving();
        }
        else
        {
            animator.SetBool("moving", false);
            //animator.Play("Idle");
        }
        if (animator.GetBool("rolling"))
        {
            rolling();
        }

    }

    // Update is called once per frame
    void Update()
    {
        //翻滚
        if (!animator.GetBool("stop") && !animator.GetBool("rolling") && !animator.IsInTransition(0) && !rollingCD)
        {
            if (Input.GetButtonDown("Jump"))
            {
                moving();
                animatorEnd();
                endWeakBoss();
                move = false;
                animator.Play("Roll");
                animator.SetBool("rolling", true);
            }
        }
        //近战or远程攻击
        if (move && Input.GetButton("Fire2") && !animator.IsInTransition(0))
        {
            aimLineStart();
            if (Input.GetButtonDown("Fire1"))
            {
                move = false;
                animator.SetBool("moving", false);
                animator.SetBool("rangeAttack", true);
            }
        }
        else if (move && Input.GetButtonDown("Fire1") && !animator.IsInTransition(0))
        {
            move = false;
            animator.SetBool("moving", false);
            animator.SetBool("nearAttack", true);
        }
        if (Input.GetButtonUp("Fire2") && !animator.IsInTransition(0))
        {
            aimLine.enabled = false;
        }

        if (comboNow)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                comboNow = false;
                animator.SetBool("combo", true);
            }
        }

    }

}
