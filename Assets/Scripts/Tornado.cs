using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum colorType1
{
    white = 0,
    red = 1,
    cyan = 2,
    black = 3
}

public class Tornado : MonoBehaviour
{


    UnityEngine.AI.NavMeshAgent nav;
    [SerializeField] float dTime;
    [SerializeField] int damage;
    [SerializeField] float existTime;
    [SerializeField] float powerDamage;
    [SerializeField] float stopTime;
    [SerializeField] colorType1 damageType;
    [SerializeField] Tornado[] otherTornado;
    bool inDamage;
    Transform playerTrs;
    float timer;


    // Use this for initialization
    void Start()
    {
        nav = GetComponent<UnityEngine.AI.NavMeshAgent>();
        playerTrs = FindObjectOfType<Player_new>().transform;
        timer = 0;
        inDamage = false;
        Destroy(gameObject, existTime);

    }

    // Update is called once per frame
    void Update()
    {
        nav.SetDestination(playerTrs.position);

        if (timer > dTime)
        {
            if (inDamage)
                playerTrs.GetComponent<Player_new>().inJured(damage, powerDamage, damageType.GetHashCode(), stopTime);
            timer = 0;
        }

        timer += Time.deltaTime;
        inDamage = false;

        otherTornado = FindObjectsOfType<Tornado>();
 

        //for (int i = 0; i < otherTornado.Length; i++)
        //{
        //    if (otherTornado[i].damageType != damageType && GetComponent<SphereCollider>().radius*2 > (otherTornado[i].transform.position - transform.position).magnitude)
        //    {
        //        Destroy(gameObject, 0);
        //    }
        //}

    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "MainCharacter")
        {
            inDamage = true;
        }
        if (other.tag == "tornado")
        {

            Destroy(gameObject);
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "MainCharacter")
        {
            inDamage = true;
        }
        if (collision.gameObject.tag == "tornado")
        {

            Destroy(gameObject);
        }
    }
}
