using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Monster : NSC_Character
{
    [Space(20), Header("小怪需求")]
    [SerializeField, Tooltip("跟随玩家距离")]
    float FollowPlayer;
    [SerializeField, Tooltip("攻击距离，近战/远程")]
    float attackDis;
    Vector3 startPosition;
    Player player;


    NavMeshAgent nav;


    // Use this for initialization
    void Start()
    {
        startPosition = transform.position;
        nav = GetComponent<NavMeshAgent>();
        player = FindObjectOfType<Player>();

    }

    // Update is called once per frame
    void Update()
    {
        if (!animator.GetBool("stop") && !animator.GetBool("attack") && !dead && !animator.IsInTransition(0))
        {
            moving();
        }
        else
        {
            nav.isStopped = true;
        }
    }

    private void moving()
    {
        float disPlayer = (player.transform.position - transform.position).magnitude;
        if (disPlayer > attackDis && disPlayer < FollowPlayer)
        {
            nav.isStopped = false;
            nav.SetDestination(player.transform.position);
            animator.SetBool("move", true);
        }
        else if (disPlayer > FollowPlayer)
        {
            if ((transform.position - startPosition).magnitude > 0.5)
            {
                nav.isStopped = false;
                nav.SetDestination(startPosition);
                animator.SetBool("move", true);
            }
            else
            {
                nav.isStopped = true;
                animator.SetBool("move", false);
            }

        }
        else if (disPlayer < attackDis)
        {
            animator.SetBool("move", false);
            animator.SetBool("attack", true);
            transform.forward = player.transform.position - transform.position;
        }
    }
    /// <summary>
    /// 动画机结束
    /// </summary>
    public virtual void allReady()
    {
        if (!animator.IsInTransition(0))
        {
            power.m_colorType = NSC_Color.colorType.white;
            animator.SetBool("attack", false);
            animator.SetBool("stop", false);

        }


    }

    /// <summary>
    /// 随机改变颜色
    /// </summary>
    public void changeColorRandom()
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
}
