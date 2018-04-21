using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_new : MonoBehaviour
{
    public Player_new player;
    public float HP;
    public float maxHP;
    public int shield;
    public int shieldMax;
    public int shieldType;
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
    public BulletAll boss_red;
    public BulletAll boss_black;
    public BulletAll boss_cyan;
    public float rangeDispersed;
    public Transform shootPoint;
    public float laserTime;
    public float laserSpeed;
    public float laserdis;
    public int laserDamage;
    public float laserPowerDamage;
    public Color laserRed;
    public Color laserCyan;
    public float laserStopTime;
    [SerializeField] float weakTime;
    [SerializeField] float weakDamageDeep;
    [SerializeField] float mode2Hp;
    [SerializeField] float flashDisBoss2;
    [SerializeField] Boss_copy bossCopy;
    [SerializeField] Renderer[] renderer;
    [SerializeField] Collider collider;
    [SerializeField] Material laserMaterialCyan;
    [SerializeField] Material laserMaterialRed;
    [SerializeField] SkinnedMeshRenderer meshRenderer;
    [SerializeField] Material bossRed;
    [SerializeField] Material bossCyan;
    [SerializeField] Material bossNormal;

    [SerializeField] GameObject shield_red;
    [SerializeField] GameObject shield_blue;




    [HideInInspector] public bool follow;
    [HideInInspector] public bool attack;
    [HideInInspector] UnityEngine.AI.NavMeshAgent nav;

    bool boss_copy;
    LineRenderer laser;
    Animator animator;
    bool laserStart;
    Vector3 laserTargetPoint;
    Vector3 laserdir;
    int laserType;
    bool laserDamaged;
    bool mode2;
    [HideInInspector] public bool specialAttack;
    int specialType;
    GameObject boss1;
    GameObject boss2;


    [SerializeField] public int bossCopyNub;

    void hideMesh()
    {
        boss_stopImmediately();
        for (int i = 0; i < renderer.Length; i++)
        {
            renderer[i].enabled = false;
        }
        collider.enabled = false;
        clearAnimator();
    }
    void clearAnimator()
    {
        animator.SetBool("nearAttack", false);
        animator.SetBool("moving", false);
        animator.SetBool("laser", false);
        animator.SetBool("weak", false);
        animator.SetBool("stop", false);
        animator.SetBool("rangeAttack", false);
        animator.SetBool("laserEnd", false);
        animator.SetBool("nowWeak", false);
        laser.enabled = false;
    }
    void showMesh()
    {
        boss_start();
        for (int i = 0; i < renderer.Length; i++)
        {
            renderer[i].enabled = true;
        }
        collider.enabled = true;
    }

    // Use this for initialization
    void Start()
    {
        nav = GetComponent<UnityEngine.AI.NavMeshAgent>();
        animator = GetComponent<Animator>();
        nav.isStopped = true;
        laser = GetComponent<LineRenderer>();
        laserStart = false;
        laser.enabled = false;
        mode2 = false;
        bossCopyNub = 0;
        specialType = 3;
        specialAttack = false;
<<<<<<< HEAD
        Dead = false;
        boss_copy = false;
=======
>>>>>>> 1351abd25d82ee847e36a3090ccf7ea6f4c79922
    }

    public void injured(int damage, int damageType)
    {
        if (specialAttack && specialType != 3)
        {
            if (damageType != specialType)
            {
                boss_stopImmediately();
                boss_weakStop();
                StartCoroutine(weakNow());
            }
        }

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
                else if (shieldType == 2)
                    shieldType = 1;
            }
            if (shield <= 0)
            {
                boss_stopImmediately();
                boss_weakStop();
                StartCoroutine(weakNow());

            }
        }
        else
        {
            HP -= damage * weakDamageDeep;
        }

    }

    // Update is called once per frame
    void Update()
    {

        if (HP < mode2Hp && !mode2)
        {
            mode2 = true;
            boss_stopImmediately();
            boss_weakStop();
            boss_start();
        }
        if (animator.GetBool("moving") && animator.GetBool("nearAttack"))
        {
            animator.SetBool("moving", false);
        }

        if (shield > 0 && !specialAttack && !boss_copy)
        {
            if(shieldType == 1)
            {
                shield_red.SetActive(true);
                shield_blue.SetActive(false);

            }
            else if(shieldType == 2)
            {
                shield_red.SetActive(false);
                shield_blue.SetActive(true);
            }
            else
            {
                shield_red.SetActive(false);
                shield_blue.SetActive(false);
            }
        }
        else
        {
            shield_red.SetActive(false);
            shield_blue.SetActive(false);
        }


    }

    IEnumerator boss_move()
    {
        clearAnimator();
        while (true)
        {

            nav.isStopped = false;
            if (attack && !animator.GetBool("nearAttack"))
            {
                boss_weakStop();
                clearAnimator();
                nav.isStopped = true;
                yield return StartCoroutine(boss_nearAttack());

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
            clearAnimator();
            StopCoroutine("boss_move");
            boss_stopImmediately();

            int a = Random.Range(0, 3);
            boss_weakStop();
            if (a == 0)
            {
                if (mode2)
                {
                    yield return StartCoroutine(boss_nearAttack1());
                }
                else
                    yield return StartCoroutine(boss_nearAttack());
            }
            else if (a == 1)
            {
                if (mode2)
                {
                    boss_copy = true;
                    yield return StartCoroutine(boss_rangeAttack1());
                    boss_copy = false;
                }
                else
                {
                    yield return StartCoroutine(boss_rangeAttack());
                }
            }
            else
            {
                if (mode2)
                {
                    boss_copy = true;
                    yield return StartCoroutine(boss_laser1());
                    boss_copy = false;
                }
                else
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
        nav.isStopped = true;

    }
    public void boss_weakStop()
    {
        StopAllCoroutines();
        animator.SetBool("nearAttack", false);
        animator.SetBool("moving", false);
        animator.SetBool("laser", false);
        animator.SetBool("weak", false);
        animator.SetBool("stop", false);
        animator.SetBool("rangeAttack", false);
        animator.SetBool("laserEnd", false);
        animator.SetBool("nowWeak", false);
        animator.Play("Idle");
        laser.enabled = false;
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
    IEnumerator boss_nearAttack1()
    {
        clearAnimator();
        animator.SetBool("nearAttack", true);
        boss_stopImmediately();
        transform.position = player.transform.position + player.transform.forward * nearFlashDis;
        transform.forward = player.transform.position - transform.position;
        while (animator.GetBool("nearAttack"))
        {
            yield return 0;
        }
        boss_start();

    }
    public void nearAttack1_start()
    {
        specialAttack = true;
        specialType = 1;
        bossRightHand.GetComponent<Collider>().enabled = true;
        bossRightHand.attackValue = nearDamage;
        bossRightHand.attackPower = nearPowerDamage;
        bossRightHand.stopTime = stoptimeDamage;
        bossRightHand.attackType = specialType;

        meshRenderer.material = bossRed;
    }
    public void nearAttack1_end()
    {
        specialAttack = false;
        bossRightHand.GetComponent<Collider>().enabled = false;
        meshRenderer.material = bossNormal;
    }
    public void nearAttack2_start()
    {
        specialAttack = true;
        specialType = 2;
        bossLeftHand.GetComponent<Collider>().enabled = true;
        bossLeftHand.attackValue = nearDamage;
        bossLeftHand.attackPower = nearPowerDamage;
        bossLeftHand.stopTime = stoptimeDamage;
        bossLeftHand.attackType = specialType;
        meshRenderer.material = bossCyan;
    }
    public void nearAttack2_end()
    {
        specialAttack = false;
        bossLeftHand.GetComponent<Collider>().enabled = false;
        meshRenderer.material = bossNormal;
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
        animator.SetBool("moving", true);
        boss_start();
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
    IEnumerator boss_rangeAttack1()
    {

        hideMesh();
        bossCopyNub = 0;
        boss1 = Instantiate(bossCopy.gameObject);
        boss1.transform.position = player.transform.position + (player.transform.forward * flashDisBoss2);
        boss1.transform.forward = player.transform.position - boss1.transform.position;
        boss2 = Instantiate(bossCopy.gameObject);
        boss2.transform.position = player.transform.position - (player.transform.forward * flashDisBoss2);
        boss2.transform.forward = player.transform.position - boss2.transform.position;
        boss1.GetComponent<Boss_copy>().boss = this;
        boss2.GetComponent<Boss_copy>().boss = this;
        boss1.GetComponent<Boss_copy>().star_rangeAttack();
        boss2.GetComponent<Boss_copy>().star_rangeAttack();
        while (bossCopyNub < 1)
        {
            print(bossCopyNub);
            yield return 0;
        }
        shield = Mathf.Max(boss1.GetComponent<Boss_copy>().shield, boss2.GetComponent<Boss_copy>().shield);
        if (boss1.GetComponent<Boss_copy>().shieldType == boss2.GetComponent<Boss_copy>().shieldType)
        {
            shieldType = boss1.GetComponent<Boss_copy>().shieldType;
        }
        else
        {
            shieldType = Random.Range(1, 3);
        }

        showMesh();
        Destroy(boss1);
        Destroy(boss2);
        boss_copy = false;
        boss_start();
    }
    public void rangeAttack_shoot()
    {
        transform.localEulerAngles += new Vector3(0, rangeDispersed, 0);
        GameObject bullet = Instantiate(boss_red.gameObject, shootPoint.position, shootPoint.rotation);
        bullet.GetComponent<Rigidbody>().velocity = shootPoint.forward * 6;

        transform.localEulerAngles -= new Vector3(0, rangeDispersed, 0) * 2;
        Instantiate(boss_cyan.gameObject, shootPoint.position, shootPoint.rotation).GetComponent<Rigidbody>().velocity = shootPoint.forward * 6;

        transform.localEulerAngles += new Vector3(0, rangeDispersed, 0);
        Instantiate(boss_black.gameObject, shootPoint.position, shootPoint.rotation).GetComponent<Rigidbody>().velocity = shootPoint.forward * 6;
        if (shieldType == 1)
            shieldType = 2;
        else if (shieldType == 2)
            shieldType = 1;


    }
    public void rangeAttack_end()
    {
        animator.SetBool("rangeAttack", false);
        boss_start();
    }

    IEnumerator boss_laser()
    {
        laserDamaged = false;
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
                    if ((hit.point - shootPoint.position).magnitude < (laserTargetPoint - shootPoint.position).magnitude)
                    {
                        if (hit.collider.gameObject.tag == "MainCharacter" && !laserDamaged)
                        {
                            player.inJured(laserDamage, laserPowerDamage, laserType, laserStopTime);
                            print(laserType);
                            laserDamaged = true;

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
    IEnumerator boss_laser1()
    {
        hideMesh();
        bossCopyNub = 0;
        boss1 = Instantiate(bossCopy.gameObject);
        boss1.transform.position = player.transform.position + (player.transform.right * flashDisBoss2);
        boss1.transform.forward = player.transform.position - boss1.transform.position;
        boss2 = Instantiate(bossCopy.gameObject);
        boss2.transform.position = player.transform.position - (player.transform.right * flashDisBoss2);
        boss2.transform.forward = player.transform.position - boss2.transform.position;
        boss1.GetComponent<Boss_copy>().boss = this;
        boss2.GetComponent<Boss_copy>().boss = this;
        boss1.GetComponent<Boss_copy>().star_laserAttack();
        boss2.GetComponent<Boss_copy>().star_laserAttack();
        if (Random.Range(0.0f, 1.0f) > 0.5)
        {
            boss1.GetComponent<Boss_copy>().laserType = 1;
            boss2.GetComponent<Boss_copy>().laserType = 2;
        }
        else
        {
            boss1.GetComponent<Boss_copy>().laserType = 2;
            boss2.GetComponent<Boss_copy>().laserType = 1;
        }

        while (bossCopyNub < 1)
        {

            yield return 0;
        }
        shield = Mathf.Max(boss1.GetComponent<Boss_copy>().shield, boss2.GetComponent<Boss_copy>().shield);
        if (boss1.GetComponent<Boss_copy>().shieldType == boss2.GetComponent<Boss_copy>().shieldType)
        {
            shieldType = boss1.GetComponent<Boss_copy>().shieldType;
        }
        else
        {
            shieldType = Random.Range(1, 3);
        }

        showMesh();
        Destroy(boss1);
        Destroy(boss2);
        boss_copy = false;
        boss_start();
    }

    public void laser_start()
    {
        laserStart = true;
        laserTargetPoint = player.transform.position - transform.right * laserdis;
        laserdir = transform.right;
        laser.enabled = true;
        if (Random.Range(0.0f, 1.0f) > 0.5)
        {
            laser.material = laserMaterialRed;
            laserType = 1;
            shieldType = 1;
        }
        else
        {
            laser.material = laserMaterialCyan;
            laserType = 2;
            shieldType = 2;
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
        laserDamaged = false;
        boss_start();
    }


    IEnumerator weakNow()
    {

        boss_stopImmediately();
        animator.SetBool("weak", true);
        animator.SetBool("nowWeak", true);
        yield return new WaitForSeconds(weakTime);

        animator.SetBool("weak", false);


    }

    public void shieldAdd()
    {
        shield = 2;
        if (Random.Range(0, 1) < 0.5)
        {
            shieldType = 1;
        }
        else
        {
            shieldType = 2;
        }

        boss_start();
    }

    public void nowWeak()
    {
        animator.SetBool("nowWeak", false);
    }

    public void weakCopy()
    {
        showMesh();
        Destroy(boss1);
        Destroy(boss2);
        boss_stopImmediately();
        boss_weakStop();
        StartCoroutine(weakNow());
    }


    public void test_bossLaser()
    {
        if (mode2)
            StartCoroutine(boss_laser1());
        else
            StartCoroutine(boss_laser());
    }
    public void test_bossNearAttack()
    {
        if (mode2)
            StartCoroutine(boss_nearAttack1());
        else
            StartCoroutine(boss_nearAttack());
    }
    public void test_bossRangeAttack()
    {
        if (mode2)
            StartCoroutine(boss_rangeAttack1());
        else

            StartCoroutine(boss_rangeAttack());

    }
    public void test_bossWeak()
    {
        shield = 0;
        boss_stopImmediately();
        boss_weakStop();
        StartCoroutine(weakNow());
    }
    public void test_bossMode2()
    {
        if (mode2)
        {
            HP = maxHP;
            mode2 = !mode2;
        }
        else
        {

            HP = mode2Hp - 1;
            mode2 = !mode2;
        }
    }
}
