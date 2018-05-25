using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : NSC_Character {
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
    Player player;
    [HideInInspector] bool m_allReady;
    [HideInInspector] int bossCopyNub;
    [HideInInspector] Vector3 HidePosition;

	// Use this for initialization
	void Start () {
        characterType = CharacterType.boss;
        player = FindObjectOfType<Player>();
	}

    public override void startNearAttack(int attackNub)
    {

        switch (attackNub)
        {
            case 0: nearAttackCollider[attackNub].enabled = true;break;
            case 1: nearAttackCollider[attackNub].enabled = true; break;
            case 2:nearAttackCollider[0].enabled = true; nearAttackCollider[1].enabled = true;break;
        }
            

    }
    public void bossHide(int a)
    {
        HidePosition = transform.position;
        transform.position = new Vector3(0, 0, -1000);
        animator.StopPlayback();
        GameObject boss1 = Instantiate(bossCopy.gameObject);
        boss1.transform.position = player.transform.position + (player.transform.right * flashDisBoss2);
        boss1.transform.forward = player.transform.position - boss1.transform.position;
        GameObject boss2 = Instantiate(bossCopy.gameObject);
        boss2.transform.position = player.transform.position - (player.transform.right * flashDisBoss2);
        boss2.transform.forward = player.transform.position - boss2.transform.position;
        if(a == 1)
        {
            boss1.GetComponent<BossCopy>().animator.SetBool("rangeAttack", true);
            boss2.GetComponent<BossCopy>().animator.SetBool("rangeAttack", true);
        }
        else
        {
            boss1.GetComponent<BossCopy>().animator.SetBool("tornado", true);
            boss2.GetComponent<BossCopy>().animator.SetBool("tornado", true);
        }
        
    }

    public void bossCopyReturn()
    {
        bossCopyNub++;
        if (bossCopyNub >= 2)
        {
            this.transform.position = HidePosition;
            animator.Play("Idle");
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
    public void allReady()
    {
        if (!animator.IsInTransition(0))
        {
            Debug.Break();
            m_allReady = true;
            power.m_colorType = NSC_Color.colorType.white;
            animator.SetBool("tornado", false);
            animator.SetBool("weak", false);
            animator.SetBool("nearAttack", false);

        }


    }
    /// <summary>
    /// 决策
    /// </summary>
    public void detectRandom()
    {
        if (mode2HP > HP)
        {
            switch (Random.Range(0, 3))
            {
                case 0: animator.SetBool("nearAttack", true); break;
                case 1: bossHide(1); break;
                case 3: bossHide(2); break;
            }
        }
        else
        {
            switch (Random.Range(0, 3))
            {
                case 0: animator.SetBool("nearAttack", true); break;
                case 1: animator.SetBool("rangeAttack", true); break;
                case 3: animator.SetBool("tornado", true); break;
            }
        }
    }
    /// <summary>
    /// boss射击
    /// </summary>
    /// <param name="typeRange"></param>
    public  void shoot(int typeRange)
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

    public void changeColor(int i_colorType)
    {
        power.m_colorType = NSC_Color.colorType.white;
    }
    // Update is called once per frame
    void Update () {
		
	}
}
