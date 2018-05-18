using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_follow : MonoBehaviour
{
    [SerializeField] Boss_new boss;
    [SerializeField] Collider[] collider;
    bool start;

    // Use this for initialization
    void Start()
    {
        for (int i = 0; i < collider.Length; i++)
            collider[i].enabled = false;
        start = false;
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "MainCharacter" && !start)
        {
            boss.follow = true;
            boss.boss_start();
            for (int i = 0; i < collider.Length; i++)
                collider[i].enabled = true;


        }
    }
    private void OnTriggerExit(Collider other)
    {
        //if (other.tag == "MainCharacter")
        //{
        //    boss.follow = false;
        //    boss.StopAllCoroutines();
        //    boss.boss_stopImmediately();
        //}
    }
}
