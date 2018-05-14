using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_copy : MonoBehaviour
{
    [HideInInspector] public Boss_new boss;
    [HideInInspector] public Player_new player;
    public int shield;
    [HideInInspector] public int shieldMax;
    public int shieldType;
    public BulletAll boss_red;
    public BulletAll boss_black;
    public BulletAll boss_cyan;
    [HideInInspector] public float rangeDispersed;
    public Transform shootPoint;
    [HideInInspector] public float laserTime;
    [HideInInspector] public float laserSpeed;
    [HideInInspector] public float laserdis;
    [HideInInspector] public int laserDamage;
    [HideInInspector] public float laserPowerDamage;
    [HideInInspector] public Color laserRed;
    [HideInInspector] public Color laserCyan;
    [HideInInspector] public float laserStopTime;
    [SerializeField] public Material laserMaterialCyan;
    [SerializeField] public Material laserMaterialRed;
    int nubRangeAttack;


    LineRenderer laser;
    [SerializeField] Animator animator;
    bool laserStart;
    Vector3 laserTargetPoint;
    Vector3 laserdir;
    public int laserType;
    bool laserDamaged;

    public void injured(int damage, int damageType)
    {

        //if (shield > 0)
        //{
        //    if (shieldType == damageType || damageType == 0)
        //    {
        //        shield = shieldMax;
        //    }
        //    else
        //    {
        //        shield--;
        //        if (shieldType == 1)
        //            shieldType = 2;
        //        else if (shieldType == 2)
        //            shieldType = 1;
        //    }
        //    if (shield <= 0)
        //    {

        //        boss.bossCopyNub += 2;
        //        boss.weakCopy();
        //    }
        //}


    }

    // Use this for initialization
    void Start()
    {
        boss = FindObjectOfType<Boss_new>();
        player = boss.player;
        shield = boss.shield;
        shieldMax = boss.shieldMax;
        shieldType = boss.shieldType;
        rangeDispersed = boss.rangeDispersed;
        laserTime = boss.laserTime;
        laserSpeed = boss.laserSpeed;
        laserdis = boss.laserdis;
        laserDamage = boss.laserDamage;
        laserPowerDamage = boss.laserPowerDamage;
        laserRed = boss.laserRed;
        laserCyan = boss.laserCyan;
        laserStopTime = boss.laserStopTime;
        animator = GetComponent<Animator>();
        laser = GetComponent<LineRenderer>();
        laserStart = false;
        laser.enabled = false;
        int nubRangeAttack = boss.nubRangeAttack;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("e"))
        {
            shield = 1;
            shieldType = 1;
            injured(0, 2);
        }
        if (Input.GetKeyDown("r"))
        {

        }

    }

    public void star_rangeAttack()
    {

        StartCoroutine(boss_rangeAttack());
    }

    IEnumerator boss_rangeAttack()
    {
        animator.SetBool("rangeAttack", true);
        animator.Play("RangeAttack");
        while (animator.GetBool("rangeAttack"))
        {
            yield return 0;
        }
    }

    public void rangeAttack_shoot(int typeRange)
    {
        int nubRangeAttack = boss.nubRangeAttack;

        for (int i = 0; i < nubRangeAttack; i++)
        {
            float M_angle = 360 / nubRangeAttack;
            if (typeRange == 1)
                Instantiate(boss_red.gameObject, shootPoint.position, shootPoint.rotation).GetComponent<Rigidbody>().velocity = shootPoint.forward * 6;
            else if (typeRange == 2)
                Instantiate(boss_cyan.gameObject, shootPoint.position, shootPoint.rotation).GetComponent<Rigidbody>().velocity = shootPoint.forward * 6;
            else if (typeRange == 3)
                Instantiate(boss_black.gameObject, shootPoint.position, shootPoint.rotation).GetComponent<Rigidbody>().velocity = shootPoint.forward * 6;
            transform.localEulerAngles += new Vector3(0, M_angle, 0);
        }

        //transform.localEulerAngles += new Vector3(0, rangeDispersed, 0);
        //GameObject bullet = Instantiate(boss_red.gameObject, shootPoint.position, shootPoint.rotation);
        //bullet.GetComponent<Rigidbody>().velocity = shootPoint.forward * 6;

        //transform.localEulerAngles -= new Vector3(0, rangeDispersed, 0) * 2;
        //Instantiate(boss_cyan.gameObject, shootPoint.position, shootPoint.rotation).GetComponent<Rigidbody>().velocity = shootPoint.forward * 6;

        //transform.localEulerAngles += new Vector3(0, rangeDispersed, 0);
        //Instantiate(boss_black.gameObject, shootPoint.position, shootPoint.rotation).GetComponent<Rigidbody>().velocity = shootPoint.forward * 6;

        if (shieldType == 1)
            shieldType = 2;
        else if (shieldType == 2)
            shieldType = 1;

    }
    public void rangeAttack_end()
    {
        animator.SetBool("rangeAttack", false);
        boss.bossCopyNub++;
    }

    public void star_laserAttack()
    {
        StartCoroutine(boss_laser());
    }
    IEnumerator boss_laser()
    {
        laserDamaged = false;
        animator.Play("Boss_laser_start");
        animator.SetBool("laser", true);
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
        laserTargetPoint = player.transform.position - transform.right * laserdis + transform.forward * laserdis;
        laserdir = transform.right;
        laser.enabled = true;
        if (laserType == 1)
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
        boss.bossCopyNub++;
    }

}
