using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_audio : MonoBehaviour
{
    [SerializeField] AudioClip weaponEmpty;
    [Range(0, 1)] [SerializeField] float emptyVolume;
    [SerializeField] AudioClip weaponBoss;
    [Range(0, 1)] [SerializeField] float bossVolume;
    [SerializeField] AudioClip weaponBossShield;
    [Range(0, 1)] [SerializeField] float bossShieldVolume;
    [SerializeField] AudioClip weaponEnemy;
    [Range(0, 1)] [SerializeField] float enemyVolume;
    [SerializeField] AudioClip weaponOther;
    [Range(0, 1)] [SerializeField] float otherVolume;
    [SerializeField] AudioClip rolling;
    [Range(0, 1)] [SerializeField] float rollingVolume;
    [SerializeField] AudioSource weaponAudio;


    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void playerAudio_weaponEmpty()
    {
        weaponAudio.Stop();
        weaponAudio.clip = weaponEmpty;
        weaponAudio.volume = emptyVolume;
        weaponAudio.Play();
    }

    public void playerAudio_weaponBoss()
    {
        weaponAudio.Stop();
        weaponAudio.clip = weaponBoss;
        weaponAudio.volume = bossVolume;
        weaponAudio.Play();
    }

    public void playerAudio_rolling()
    {
        weaponAudio.Stop();
        weaponAudio.clip = rolling;
        weaponAudio.volume = rollingVolume;
        weaponAudio.Play();
    }

    public void playerAudio_weaponOther()
    {
        weaponAudio.Stop();
        weaponAudio.clip = weaponOther;
        weaponAudio.volume = otherVolume;
        weaponAudio.Play();
    }

    public void playerAudio_weaponEnemy()
    {
        weaponAudio.Stop();
        weaponAudio.clip = weaponEnemy;
        weaponAudio.volume = enemyVolume;
        weaponAudio.Play();
    }

    public void playerAudio_weaponBossShield()
    {
        weaponAudio.Stop();
        weaponAudio.clip = weaponBossShield;
        weaponAudio.volume = bossShieldVolume;
        weaponAudio.Play();
    }
}
