using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(CharacterController))]
public class Player : NSC_Character {
    [Space(20)]
    [Header("角色移动")]
    [Tooltip("移动速度")]
    public float speed;
    [Tooltip("翻滚速度")]
    public float rollingSpeed;

    [Header("特殊状态")]
    [Tooltip("其他颜色槽")]
    public NSC_Color otherPower;

    CharacterController characterController;
    bool move;//角色是否可移动


    // Use this for initialization
    void Start () {
        characterController = GetComponent<CharacterController>();
        move = true;
	}

    public override bool injured(Attack attack)
    {
        if (!animator.GetBool("rolling"))
        {
            
            HP -= attack.damage;
            HPNormal();
            if (!dead && characterType != CharacterType.boss && attack.stopTime > 0.0f)
            {
                StopCoroutine("characterStop");
                StartCoroutine("characterStop", attack);
            }
            return true;
        }
        return false;
    }

    // Update is called once per frame
    void Update () {
        if (!animator.GetBool("stop") && !animator.GetBool("rolling") && !animator.IsInTransition(0))
        {
            if (Input.GetButtonDown("Jump"))
            {
                move = false;
                animator.Play("Roll");
                animator.SetBool("rolling", true);
            }
        }
		
	}
    /// <summary>
    /// 翻滚结束Event。
    /// </summary>
    public void rollingEnd()
    {
        move = true;
        animator.SetBool("rolling", false);
    }

    /// <summary>
    /// 所有状态Idle状态空闲
    /// </summary>
    public void allReady()
    {
        move = true;
        animator.SetBool("rolling", false);
        animator.SetBool("nearAttack", false);
        animator.SetBool("rangeAttack", false);
        animator.SetBool("combo", false);
    }
    private void FixedUpdate()
    {
        if (move)
        {
            moving();
        }
        if (animator.GetBool("rolling"))
        {
            rolling();
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
        if(moveV3 != Vector3.zero)
        {
            transform.forward = moveV3;
            characterController.SimpleMove(moveV3);
            animator.SetBool("moving", true);
        }
        else
        {
            animator.SetBool("moving", false);

        }

    }
}
