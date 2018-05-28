using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(UnityEngine.AI.NavMeshAgent))]
[RequireComponent(typeof(Attack))]
[RequireComponent(typeof(Rigidbody))]
public class Tornado : MonoBehaviour
{
    UnityEngine.AI.NavMeshAgent nav;
    Player player;
    float timer;
    [Tooltip("旋风存在时间")]
    [SerializeField] float existTime;
    [Tooltip("旋风每一跳伤害")]
    public Attack attack;
    [Tooltip("旋风跳伤间隔")]
    [SerializeField] float timeDamage;
    
    // Use this for initialization
    void Start()
    {
        timer = 1000;
        nav = GetComponent<UnityEngine.AI.NavMeshAgent>();
        player = FindObjectOfType<Player>();
        GetComponent<Rigidbody>().isKinematic = true;
        if(!attack)
        {
            attack = GetComponent<Attack>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        nav.SetDestination(player.transform.position);
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.GetComponent<Tornado>())
        {
            print(NSC_Color.colorContrary(other.GetComponent<Tornado>().attack.powerDamage, attack.powerDamage));
            if (NSC_Color.colorContrary(other.GetComponent<Tornado>().attack.powerDamage, attack.powerDamage))
            {
                Destroy(gameObject);
            }
        }
    }



}
