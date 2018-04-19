using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_audio : MonoBehaviour
{
    enum EnemyAudio
    {
        weaponAttack = 1,
        weaponPlay = 2,
        rangeAttack = 3,
        dead = 4
    }

    [SerializeField] AudioClip weaponAttack;
    [Range(0, 1)] [SerializeField] float weaponAttackVolume;
    [SerializeField] AudioClip weaponPlay;
    [Range(0, 1)] [SerializeField] float weaponPlayVolume;
    [SerializeField] AudioClip rangeAttack;
    [Range(0, 1)] [SerializeField] float rangeAttackVolume;
    [SerializeField] AudioClip dead;
    [Range(0, 1)] [SerializeField] float deadVolume;

    [SerializeField] AudioSource audioSource;



    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void enemy_Audio(int a)
    {
       audioSource.Stop();
        switch (a)
        {
            case 1:
                audioSource.clip = weaponAttack;
                audioSource.volume = weaponAttackVolume;
                break;
            case 2:
                audioSource.clip = weaponPlay;
                audioSource.volume = weaponPlayVolume;
                break;
            case 3:
                audioSource.clip = rangeAttack;
                audioSource.volume = rangeAttackVolume;
                break;
            case 4:
                audioSource.clip = dead;
                audioSource.volume = deadVolume;
                break;

        }
        audioSource.Play();
    }
}
