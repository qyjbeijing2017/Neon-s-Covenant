using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] int HP;
    [SerializeField] int HPmax;
    [SerializeField] int damage;
    [SerializeField] float damagePower;
    [SerializeField] float damageStop;
    [SerializeField] bool rangeEnemy;
    [SerializeField] BulletAll bullet_red;
    [SerializeField] BulletAll bullet_cyan;
    [SerializeField] float damageDeep;
    [SerializeField] Player_new player;
    [SerializeField] Transform shootPoint;
    [SerializeField] Collider sword;
    [SerializeField] float attackCD;
    [SerializeField] Material normalMaterial;
    [SerializeField] Material redMaterial;
    [SerializeField] Material cyanMaterial;
    [SerializeField] SkinnedMeshRenderer meshRenderer;


    [HideInInspector] public bool dead;
    [HideInInspector] public bool attack;

    int myType;
    bool specialDamage;
    float specialType;
    Animator animator;
    UnityEngine.AI.NavMeshAgent nav;
    // Use this for initialization
    void Start()
    {
        nav = GetComponent<UnityEngine.AI.NavMeshAgent>();
        specialDamage = false;
        nav.isStopped = true;
        animator = GetComponent<Animator>();
        player = GameObject.Find("MainCharacter").GetComponent<Player_new>();
        if (!rangeEnemy)
        {
            sword.GetComponent<Enemy_weapon>().damage = damage;
            sword.GetComponent<Enemy_weapon>().damagePower = damagePower;
            sword.GetComponent<Enemy_weapon>().damageStop = damageStop;
        }
        dead = false;

    }

    public void injured(int damage, int damageType, float stopTime)
    {
        if (sword != null)
            sword.enabled = false;
        if (specialDamage)
        {
            if (damageType == 1 || damageType == 2)
            {
                if (damageType != specialType)
                {
                    HP -= (int)(damage * damageDeep);
                }
                else
                {
                    HP -= damage;
                }
            }
            else
            {
                HP -= damage;
            }
        }
        else
        {
            HP -= damage;
        }

        if (stopTime > 0)
        {
            enemy_stopImmediately();
            StartCoroutine(enemy_stop(stopTime));
        }



        if (HP <= 0)
        {
            dead = true;
            enemy_stopImmediately();
            animator.SetBool("die", true);
            animator.SetBool("isDie", true);
        }
        else if (HP >= HPmax)
        {
            HP = HPmax;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (HP <= 0)
        {
            dead = true;
            enemy_stopImmediately();
            animator.SetBool("die", true);
            animator.SetBool("isDie", true);
        }
    }

    IEnumerator enemy_move()
    {

        while (true)
        {
            if (!attack)
            {
                nav.SetDestination(player.transform.position);
                nav.isStopped = false;
                animator.SetBool("moving", true);
            }
            else
            {
                if (rangeEnemy)
                    yield return StartCoroutine(enemy_shoot());
                else
                    yield return StartCoroutine(enemy_attack());

            }

            yield return 0;
        }

    }

    IEnumerator enemy_stop(float stopTime)
    {
        animator.SetBool("delayStop", false);
        animator.SetBool("delay", true);
        yield return new WaitForSeconds(stopTime);
        animator.SetBool("delayStop", true);
        StartCoroutine("enemy_move");
    }

    public void enemy_delayFalse()
    {
        animator.SetBool("delay", false);
    }

    IEnumerator enemy_shoot()
    {
        transform.forward = player.transform.position - transform.position;
        animator.SetBool("moving", false);
        animator.SetBool("attack", true);
        nav.isStopped = true;
        if (Random.Range(0.0f, 1.0f) <= 0.5)
        {

            meshRenderer.material = redMaterial;


            myType = 1;
        }
        else
        {

            meshRenderer.material = cyanMaterial;


            myType = 2;
        }


        while (animator.GetBool("attack"))
        {
            yield return 0;
        }
        yield return new WaitForSeconds(attackCD);

    }

    IEnumerator enemy_attack()
    {
        transform.forward = player.transform.position - transform.position;
        animator.SetBool("moving", false);
        animator.SetBool("attack", true);
        nav.isStopped = true;

        if (Random.Range(0.0f, 1.0f) <= 0.5)
        {

            meshRenderer.material = redMaterial;


            myType = 1;
        }
        else
        {

            meshRenderer.material = cyanMaterial;

            myType = 2;
        }

        while (animator.GetBool("attack"))
        {
            yield return 0;
        }
        yield return new WaitForSeconds(attackCD);
    }

    public void enemy_rangeAttack()
    {
        if (myType == 1)
        {
            GameObject bullet1 = Instantiate(bullet_red.gameObject);
            bullet1.transform.position = shootPoint.position;
            bullet1.transform.rotation = bullet1.transform.rotation;
            bullet1.GetComponent<Rigidbody>().velocity = transform.forward;
        }
        else
        {
            GameObject bullet1 = Instantiate(bullet_cyan.gameObject);
            bullet1.transform.position = shootPoint.position;
            bullet1.transform.rotation = bullet1.transform.rotation;
            bullet1.GetComponent<Rigidbody>().velocity = transform.forward;
        }

    }

    public void enemy_attackend()
    {
        animator.SetBool("attack", false);

        meshRenderer.material = normalMaterial;


    }

    public void attack_start()
    {
        if (myType == 1)
        {
            sword.GetComponent<Enemy_weapon>().damageType = 1;

        }
        else
        {
            sword.GetComponent<Enemy_weapon>().damageType = 2;


        }


        sword.enabled = true;

    }
    public void attack_end()
    {
        sword.enabled = false;
    }


    public void enemy_start()
    {
        enemy_stopImmediately();
        StartCoroutine("enemy_move");

    }
    public void enemy_stopImmediately()
    {
        if (!rangeEnemy)
            sword.enabled = false;
        nav.isStopped = true;
        StopAllCoroutines();
        animator.SetBool("moving", false);
        animator.SetBool("attack", false);
        animator.SetBool("die", false);
        animator.SetBool("delay", false);
        animator.SetBool("delayStop", false);
    }


    public void enemy_dead()
    {
        Destroy(this.gameObject, 0.3f);
    }
    public void enemy_isDead()
    {
        animator.SetBool("isDie", false);
    }
}
