using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class FollowCapsule : MonoBehaviour
{
    [SerializeField] Player player;
    NavMeshAgent nav;
    [SerializeField] float dis;



    // Use this for initialization
    void Start()
    {
        nav = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        Follow();

    }


    private void FixedUpdate()
    {

    }

    public void Follow()
    {

        if ((player.transform.position - transform.position).magnitude - dis > 0)
        {
            nav.SetDestination((player.transform.position - transform.position).normalized * ((player.transform.position - transform.position).magnitude - dis) + transform.position);

        }
    }


    public void RePlayer()
    {
        player.transform.position = transform.position;
        player.animator.Play("ReFall");
        player.move = false;
    }
}
