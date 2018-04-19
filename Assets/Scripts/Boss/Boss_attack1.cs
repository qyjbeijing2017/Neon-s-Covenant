using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_attack1 : MonoBehaviour
{
    enum BossAudio
    {
        empty = 1,
        powerful = 2,
        weapon = 3,
        laser = 4,
        range = 5,
    }
    [HideInInspector] public int attackValue;
    [HideInInspector] public int attackType;
    [HideInInspector] public float attackPower;
    [HideInInspector] public float stopTime;
    public Boss_audio bossAudio;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "MainCharacter")
        {
            other.GetComponent<Player_new>().inJured(attackValue, attackPower, attackType, stopTime);
            bossAudio.bossAudio_play(BossAudio.powerful.GetHashCode());
        }
    }
}
