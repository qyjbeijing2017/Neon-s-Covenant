using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_new : MonoBehaviour
{
    [SerializeField] Player_new player;
    public float HP;
    public float maxHP;
    [SerializeField] int shield;
    [SerializeField] int shieldMax;
    [SerializeField] int shieldType;
    [SerializeField] Boss_attack1 bossLeftHand;
    [SerializeField] Boss_attack1 bossRightHand;
    [SerializeField] int nearDamage;
    [SerializeField] float nearPowerDamage;
    [SerializeField] float stoptimeDamage;
    [SerializeField] int nearDamagePowerful;
    [SerializeField] float nearPowerDamagePowerful;
    [SerializeField] float stoptimeDamagePowerful;
    [SerializeField] float detectTime;
    [SerializeField] float nearFlashDis;
    [SerializeField] BulletAll boss_red;
    [SerializeField] BulletAll boss_black;
    [SerializeField] BulletAll boss_cyan;
    [SerializeField] GameObject Box;
    [SerializeField] float rangeDispersed;
    [SerializeField] Transform shootPoint;
    [SerializeField] float laserTime;
    [SerializeField] float laserSpeed;
    [SerializeField] float laserdis;
    [SerializeField] int laserDamage;
    [SerializeField] float laserPowerDamage;
    [SerializeField] Color laserRed;
    [SerializeField] Color laserCyan;
    [SerializeField] float laserStopTime;


    [HideInInspector] public bool follow;
    [HideInInspector] public bool attack;
    [HideInInspector] UnityEngine.AI.NavMeshAgent nav;

    LineRenderer laser;
    Animator animator;
    bool laserStart;
    Vector3 laserTargetPoint;
    Vector3 laserdir;
    int laserType;
    bool laserDamaged;

    // Use this for initialization
    void Start()
    {
        nav = GetComponent<UnityEngine.AI.NavMeshAgent>();
        animator = GetComponent<Animator>();
        nav.isStopped = true;
        laser = GetComponent<LineRenderer>();
        laserStart = false;
        laser.enabled = false;
    }

    public void injured(float damage, int damageType)
    {
        if (shield > 0)
        {
            if (shieldType == damageType || damageType == 0)
            {
                shield = shieldMax;
            }
            else
            {
                shield--;
                if (shieldType == 1)
                    shieldType = 2;
                if (shieldType == 2)
                    shieldType = 1;
            }

        }
        else
        {
            HP -= damage;
        }

    }

    // Update is called once per frame
    void Update()
    {



    }

    IEnumerator boss_move()
    {

        while (true)
        {
            nav.isStopped = false;
            if (attack)
            {
                nav.isStopped = true;
                StopCoroutine("boss_Detect");
                yield return StartCoroutine(boss_nearAttack());
                StartCoroutine("boss_Detect");
                continue;
            }

            if (follow)
            {
                nav.SetDestination(player.transform.position);
                animator.SetBool("moving", true);
            }
            else
            {
                boss_stopImmediately();
            }

            yield return 0;
        }


    }

    IEnumerator boss_Detect()
    {
        while (true)
        {
            yield return new WaitForSeconds(detectTime);
            StopCoroutine("boss_move");
            boss_stopImmediately();

            int a = Random.Range(0, 3);
            if (a == 0)
            {
                yield return StartCoroutine(boss_nearAttack());
            }
            else if (a == 1)
            {
                yield return StartCoroutine(boss_rangeAttack());
            }
            else
            {
                yield return StartCoroutine(boss_laser());
            }
            StartCoroutine("boss_move");
        }
    }

    public void boss_start()
    {
        StartCoroutine("boss_move");
        StartCoroutine("boss_Detect");
    }

    public void boss_stopImmediately()
    {
        animator.SetBool("moving", false);

        nav.isStopped = true;
    }

    IEnumerator boss_nearAttack()
    {
        animator.SetBool("nearAttack", true);
        boss_stopImmediately();
        transform.position = player.transform.position + (transform.position - player.transform.position).normalized * nearFlashDis;
        transform.forward = player.transform.position - transform.position;
        while (animator.GetBool("nearAttack"))
        {
            yield return 0;
        }
    }
    public void nearAttack1_start()
    {
        bossRightHand.GetComponent<Collider>().enabled = true;
        bossRightHand.attackValue = nearDamage;
        bossRightHand.attackPower = nearPowerDamage;
        bossRightHand.stopTime = stoptimeDamage;
        bossRightHand.attackType = 1;
    }
    public void nearAttack1_end()
    {
        bossRightHand.GetComponent<Collider>().enabled = false;
    }
    public void nearAttack2_start()
    {
        bossLeftHand.GetComponent<Collider>().enabled = true;
        bossLeftHand.attackValue = nearDamage;
        bossLeftHand.attackPower = nearPowerDamage;
        bossLeftHand.stopTime = stoptimeDamage;
        bossLeftHand.attackType = 2;
    }
    public void nearAttack2_end()
    {
        bossLeftHand.GetComponent<Collider>().enabled = false;
    }
    public void nearAttack3_start()
    {
        bossRightHand.GetComponent<Collider>().enabled = true;
        bossLeftHand.GetComponent<Collider>().enabled = true;
        bossRightHand.attackValue = nearDamagePowerful / 2;
        bossRightHand.attackPower = nearPowerDamagePowerful / 2;
        bossRightHand.stopTime = stoptimeDamage;
        bossRightHand.attackType = 3;
        bossLeftHand.attackValue = nearDamagePowerful / 2;
        bossLeftHand.attackPower = nearPowerDamagePowerful / 2;
        bossLeftHand.stopTime = stoptimeDamage;
        bossLeftHand.attackType = 3;
    }
    public void nearAttack3_end()
    {
        bossRightHand.GetComponent<Collider>().enabled = false;
        bossLeftHand.GetComponent<Collider>().enabled = false;

    }
    public void nearAttack_end()
    {
        animator.SetBool("nearAttack", false);

    }


    IEnumerator boss_rangeAttack()
    {
        animator.SetBool("rangeAttack", true);
        boss_stopImmediately();
        while (animator.GetBool("rangeAttack"))
        {
            yield return 0;
        }
    }

    public void rangeAttack_shoot()
    {
        transform.localEulerAngles += new Vector3(0, rangeDispersed, 0);
        GameObject bullet = Instantiate(boss_red.gameObject, shootPoint.position , shootPoint.rotation);
        bullet.GetComponent<Rigidbody>().velocity = shootPoint.forward * 6;

        transform.localEulerAngles -= new Vector3(0, rangeDispersed, 0) * 2;
        Instantiate(boss_cyan.gameObject, shootPoint.position, shootPoint.rotation).GetComponent<Rigidbody>().velocity = shootPoint.forward * 6;

        transform.localEulerAngles += new Vector3(0, rangeDispersed, 0);
        Instantiate(boss_black.gameObject, shootPoint.position, shootPoint.rotation).GetComponent<Rigidbody>().velocity = shootPoint.forward * 6;


    }
    public void rangeAttack_end()
    {
        animator.SetBool("rangeAttack", false);
    }

    IEnumerator boss_laser()
    {
        animator.SetBool("laser", true);
        boss_stopImmediately();
        while (animator.GetBool("laser"))
        {
            if (laserStart)
            {
                transform.forward = laserTargetPoint - transform.position;
                Ray ray = new Ray(shootPoint.position, shootPoint.forward);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, 100f))
                {
                    if((hit.point-shootPoint.position).magnitude<(laserTargetPoint - shootPoint.position).magnitude)
                    {
                        if (hit.collider.gameObject.tag == "MainCharacter" && !laserDamaged)
                        {
                            player.inJured(laserDamage, laserPowerDamage, laserType, laserStopTime);
                            laserDamaged = false;
                        }
                        laser.SetPosition(1, hit.point);
                    }
                    else
                    {
                        laser.SetPosition(1, laserTargetPoint);
                    }
                    
                }
                else
                {
                    laser.SetPosition(1, laserTargetPoint);
                }
                laser.SetPosition(0, shootPoint.position);



                laserTargetPoint += laserdir.normalized * laserSpeed * Time.deltaTime;
                transform.forward = laserTargetPoint - transform.position;

            }
            yield return 0;
        }

    }

    public void laser_start()
    {
        laserStart = true;
        laserTargetPoint = player.transform.position - transform.right * laserdis;
        laserdir = transform.right;
        laser.enabled = true;
        if (Random.Range(0, 1) > 0.5)
        {
            laser.material.color = laserRed;
            laserType = 1;
        }
        else
        {
            laser.material.color = laserCyan;
            laserType = 2;
        }
        laserDamaged = false;
        StartCoroutine(laser_time());
        
    }
    IEnumerator laser_time()
    {
        yield return new WaitForSeconds(laserTime);
        laserStart = false;
        laser.enabled = false;
        animator.SetBool("laserEnd", true);
    }
    public void laser_end()
    {
        animator.SetBool("laserEnd", false);
        animator.SetBool("laser", false);
    }

}
