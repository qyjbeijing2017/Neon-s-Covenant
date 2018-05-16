using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_new : MonoBehaviour
{
    enum colorType
    {
        white = 0,
        red = 1,
        cyan = 2,
        black = 3
    }

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
    [SerializeField] float flashWaitTime;
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
    public float laserdis1;
    public int laserDamage;
    public float laserPowerDamage;
    public Color laserRed;
    public Color laserCyan;
    public float laserStopTime;
    [SerializeField] float weakTime;
    public float colorDamagein;
    public float colorDamagede;

    [SerializeField] float mode2Hp;
    [SerializeField] float flashDisBoss2;
    [SerializeField] Boss_copy bossCopy;
    [SerializeField] Renderer[] renderer;
    [SerializeField] Collider collider;
    public Material laserMaterialCyan;
    public Material laserMaterialRed;
    [SerializeField] SkinnedMeshRenderer meshRenderer;
    public Material bossRed;
    public Material bossCyan;
    public Material bossNormal;

    [SerializeField] GameObject shield_red;
    [SerializeField] GameObject shield_blue;
    public int nubRangeAttack;




    [HideInInspector] public bool follow;
    public bool attack;
    [HideInInspector] UnityEngine.AI.NavMeshAgent nav;
    [HideInInspector] public bool Dead;

    bool boss_copy;
    LineRenderer laser;
    Animator animator;
    bool laserStart;
    Vector3 laserTargetPoint;
    Vector3 laserdir;
    int laserType;
    [SerializeField] bool laserDamaged;
    bool mode2;
    public bool specialAttack;
    int specialType;
    GameObject boss1;
    GameObject boss2;



    public int bossCopyNub;

    public float angleRange;

    int lastAttack;

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
        Dead = false;
        boss_copy = false;
        lastAttack = 4;
    }

    public void injured(int damage, int damageType)
    {
        if (damageType == shieldType && damageType != 0 && damageType != 3)
        {
            HP -= colorDamagede * damage;
        }
        else if (damageType != shieldType && damageType != 0 && damageType != 3)
        {
            HP -= colorDamagein * damage;
        }
        else
        {
            HP -= damage;
        }
    }


    public void boss_weakNow()
    {
        boss_stopImmediately();
        boss_weakStop();
        StartCoroutine(weakNow());

    }


    // Update is called once per frame
    void Update()
    {

        if (HP < mode2Hp && !mode2)
        {
            mode2 = true;

        }
        if (animator.GetBool("moving") && animator.GetBool("nearAttack"))
        {
            animator.SetBool("moving", false);
        }
        if (HP <= 0)
        {
            Dead = true;
            boss_dead();
        }



        if (shieldType == 1)
            meshRenderer.material = bossRed;
        else if (shieldType == 2)
            meshRenderer.material = bossCyan;
        else
            meshRenderer.material = bossNormal;



        if (!follow)
        {
            animator.Play("Idle");
            clearAnimator();
            GetComponent<AudioSource>().Stop();
            shield = 2;
            bossCopyNub = 0;
            showMesh();
            if (boss1 != null && boss2 != null)
            {
                shield = Mathf.Max(boss1.GetComponent<Boss_copy>().shield, boss2.GetComponent<Boss_copy>().shield);
                Destroy(boss1);
                Destroy(boss2);
            }

            boss_copy = false;
        }


    }

    IEnumerator boss_move()
    {
        clearAnimator();
        while (true)
        {

            nav.isStopped = false;
            //if (attack && !animator.GetBool("nearAttack"))
            //{
            //    StopAllCoroutines();
            //    clearAnimator();
            //    nav.isStopped = true;
            //    yield return StartCoroutine(boss_nearAttack());


            //}

            if (follow)
            {
                //nav.SetDestination(player.transform.position);
                //animator.SetBool("moving", true);
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
        yield return new WaitForSeconds(detectTime);
        clearAnimator();
        boss_stopImmediately();

        int a = Random.Range(0, 3);

        while (a == lastAttack)
        {
            a = Random.Range(0, 3);
        }

        StopAllCoroutines();
        if (!animator.GetBool("nearAttack"))
        {
            if (a == 0 && lastAttack != 0)
            {
                if (mode2)
                {
                    lastAttack = 0;
                    yield return StartCoroutine(boss_nearAttack());

                }
                else
                {
                    lastAttack = 0;
                    yield return StartCoroutine(boss_nearAttack());

                }

            }
            else if (a == 1 && lastAttack != 1)
            {
                if (mode2)
                {
                    lastAttack = 1;
                    boss_copy = true;
                    yield return StartCoroutine(boss_rangeAttack1());
                    boss_copy = false;

                }
                else
                {
                    lastAttack = 1;
                    yield return StartCoroutine(boss_rangeAttack());

                }
            }
            else if (a == 2 && lastAttack != 2)
            {
                if (mode2)
                {
                    lastAttack = 2;
                    boss_copy = true;
                    yield return StartCoroutine(boss_laser1());
                    boss_copy = false;

                }
                else
                {
                    lastAttack = 2;
                    yield return StartCoroutine(boss_laser());

                }

            }

        }


    }

    public void boss_start()
    {
        bossCopyNub = 0;
        showMesh();
        if (boss1 != null && boss2 != null)
        {
            shield = Mathf.Max(boss1.GetComponent<Boss_copy>().shield, boss2.GetComponent<Boss_copy>().shield);
            Destroy(boss1);
            Destroy(boss2);
        }

        boss_copy = false;
        StartCoroutine("boss_move");
        StartCoroutine("boss_Detect");
    }

    public void boss_stopImmediately()
    {
        nav.isStopped = true;
        bossLeftHand.enabled = false;
        bossRightHand.enabled = false;

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
        boss_stopImmediately();
        StopAllCoroutines();
        animator.SetBool("nearAttack", true);
        while (animator.GetBool("nearAttack"))
        {
            yield return 0;
        }


    }
    IEnumerator boss_nearAttack1()
    {
        clearAnimator();
        boss_stopImmediately();
        transform.position = player.transform.position + player.transform.forward * nearFlashDis;
        transform.forward = new Vector3(player.transform.position.x - transform.position.x, 0, player.transform.position.z - transform.position.z);

        yield return new WaitForSeconds(flashWaitTime);
        animator.SetBool("nearAttack", true);
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
        bossRightHand.attackType = shieldType;


    }
    public void nearAttack1_end()
    {
        specialAttack = false;
        bossRightHand.GetComponent<Collider>().enabled = false;

    }
    public void nearAttack2_start()
    {

        specialAttack = true;
        specialType = 2;
        bossLeftHand.GetComponent<Collider>().enabled = true;
        bossLeftHand.attackValue = nearDamage;
        bossLeftHand.attackPower = nearPowerDamage;
        bossLeftHand.stopTime = stoptimeDamage;
        bossLeftHand.attackType = shieldType;

    }
    public void nearAttack2_end()
    {
        specialAttack = false;
        bossLeftHand.GetComponent<Collider>().enabled = false;
    }
    public void nearAttack3_start()
    {

        bossRightHand.GetComponent<Collider>().enabled = true;
        bossLeftHand.GetComponent<Collider>().enabled = true;
        bossRightHand.attackValue = nearDamagePowerful / 2;
        bossRightHand.attackPower = nearPowerDamagePowerful / 2;
        bossRightHand.stopTime = stoptimeDamage;
        bossRightHand.attackType = shieldType;
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

    public void nearAttack_flash()
    {
        Vector3 positionNew = player.transform.position + (transform.position - player.transform.position).normalized * nearFlashDis;
        transform.position = new Vector3(positionNew.x, transform.position.y, positionNew.z);
        transform.forward = new Vector3(player.transform.position.x - transform.position.x, 0, player.transform.position.z - transform.position.z);

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
        boss_copy = true;
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
        boss1.GetComponent<Boss_copy>().shield = shield;
        boss2.GetComponent<Boss_copy>().shield = shield;
        boss1.GetComponent<Boss_copy>().star_rangeAttack();
        boss2.GetComponent<Boss_copy>().star_rangeAttack();

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


        while (bossCopyNub < 2)
        {
            yield return 0;
        }
        shield = Mathf.Max(boss1.GetComponent<Boss_copy>().shield, boss2.GetComponent<Boss_copy>().shield);
        //if (boss1.GetComponent<Boss_copy>().shieldType == boss2.GetComponent<Boss_copy>().shieldType)
        //{
        //    shieldType = boss1.GetComponent<Boss_copy>().shieldType;
        //}
        //else
        //{
        //    shieldType = Random.Range(1, 3);
        //}

        showMesh();
        Destroy(boss1);
        Destroy(boss2);
        boss_copy = false;
        boss_start();
    }
    public void rangeAttack_shoot(int typeRange)
    {

        for (int i = 0; i < nubRangeAttack; i++)
        {
            transform.localEulerAngles += new Vector3(0, angleRange * typeRange, 0);
            float M_angle = 360 / nubRangeAttack;
            if (typeRange == 1)
                Instantiate(boss_red.gameObject, shootPoint.position, shootPoint.rotation).GetComponent<Rigidbody>().velocity = shootPoint.forward * 6;
            else if (typeRange == 2)
                Instantiate(boss_cyan.gameObject, shootPoint.position, shootPoint.rotation).GetComponent<Rigidbody>().velocity = shootPoint.forward * 6;
            else if (typeRange == 3)
                Instantiate(boss_black.gameObject, shootPoint.position, shootPoint.rotation).GetComponent<Rigidbody>().velocity = shootPoint.forward * 6;
            transform.localEulerAngles += new Vector3(0, M_angle, 0);
            transform.localEulerAngles -= new Vector3(0, angleRange * typeRange, 0);
        }

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
        if (Random.Range(0.0f, 1.0f) > 0.5)
        {
            laserType = 1;
            shieldType = 1;
            laser.material = laserMaterialRed;
        }
        else
        {
            laserType = 2;
            shieldType = 2;
            laser.material = laserMaterialCyan;
        }

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
        boss_copy = true;
        bossCopyNub = 0;
        boss1 = Instantiate(bossCopy.gameObject);
        boss1.transform.position = player.transform.position + (player.transform.right * flashDisBoss2);
        boss1.transform.forward = player.transform.position - boss1.transform.position;
        boss2 = Instantiate(bossCopy.gameObject);
        boss2.transform.position = player.transform.position - (player.transform.right * flashDisBoss2);
        boss2.transform.forward = player.transform.position - boss2.transform.position;
        boss1.GetComponent<Boss_copy>().boss = this;
        boss2.GetComponent<Boss_copy>().boss = this;
        boss1.GetComponent<Boss_copy>().shield = shield;
        boss2.GetComponent<Boss_copy>().shield = shield;
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

        while (bossCopyNub < 2)
        {

            yield return 0;
        }

        shield = Mathf.Max(boss1.GetComponent<Boss_copy>().shield, boss2.GetComponent<Boss_copy>().shield);
        //if (boss1.GetComponent<Boss_copy>().shieldType == boss2.GetComponent<Boss_copy>().shieldType)
        //{
        //    shieldType = boss1.GetComponent<Boss_copy>().shieldType;
        //}
        //else
        //{
        //    shieldType = Random.Range(1, 3);
        //}

        showMesh();
        Destroy(boss1);
        Destroy(boss2);
        boss_copy = false;
        boss_start();
    }

    public void laser_start()
    {
        laserStart = true;
        transform.forward = player.transform.position - transform.position;
        laserTargetPoint = player.transform.position - transform.right * laserdis + transform.forward * laserdis;
        laserdir = transform.right;
        laser.enabled = true;

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
        changeColor(colorType.white.GetHashCode());
        boss_start();
    }


    IEnumerator weakNow()
    {


        boss_stopImmediately();
        bossLeftHand.GetComponent<Collider>().enabled = false;
        bossRightHand.GetComponent<Collider>().enabled = false;
        meshRenderer.material = bossNormal;
        animator.SetBool("weak", true);
        animator.SetBool("nowWeak", true);
        yield return new WaitForSeconds(weakTime);
        shieldType = 0;
        animator.SetBool("weak", false);


    }

    public void shieldAdd()
    {

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

    public void boss_dead()
    {
        boss_stopImmediately();

    }



    public void changeColor(int i_colorType)
    {
        shieldType = i_colorType;
    }

    public void changeColor(string s_colorType)
    {
        if (s_colorType == colorType.cyan.ToString())
            shieldType = colorType.cyan.GetHashCode();

        else if (s_colorType == colorType.red.ToString())
            shieldType = colorType.red.GetHashCode();
    }
    public void changeColor()
    {
        if (shieldType == colorType.cyan.GetHashCode())
            shieldType = colorType.red.GetHashCode();

        else if (shieldType == colorType.red.GetHashCode())
            shieldType = colorType.cyan.GetHashCode();
    }
    public void changeColorRandom()
    {
        if (Random.Range(0, 2) < 1)
        {
            shieldType = 1;
        }
        else
        {
            shieldType = 2;
        }
    }


    public void test_bossLaser()
    {
        StopAllCoroutines();
        boss_stopImmediately();
        if (mode2)
            StartCoroutine(boss_laser1());
        else
            StartCoroutine(boss_laser());
    }
    public void test_bossNearAttack()
    {
        StopAllCoroutines();
        boss_stopImmediately();
        if (mode2)
            StartCoroutine(boss_nearAttack1());
        else
            StartCoroutine(boss_nearAttack());
    }
    public void test_bossRangeAttack()
    {
        StopAllCoroutines();
        boss_stopImmediately();
        if (mode2)
            StartCoroutine(boss_rangeAttack1());
        else

            StartCoroutine(boss_rangeAttack());

    }
    public void test_bossWeak()
    {
        boss_weakNow();
    }
    public void test_bossMode2()
    {
        StopAllCoroutines();
        boss_stopImmediately();
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
