using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_audio : MonoBehaviour
{
    [SerializeField] AudioClip weaponEmpty;
    [SerializeField] AudioClip weaponBoss;
    [SerializeField] AudioClip weaponBossShield;
    [SerializeField] AudioClip weaponEnemy;
    [SerializeField] AudioClip weaponOther;
    [SerializeField] AudioClip rolling;
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
        weaponAudio.Play();
    }

    public void playerAudio_weaponBoss()
    {
        weaponAudio.Stop();
        weaponAudio.clip = weaponBoss;
        weaponAudio.Play();
    }

    public void playerAudio_rolling()
    {
        weaponAudio.Stop();
        weaponAudio.clip = rolling;
        weaponAudio.Play();
    }

    public void playerAudio_weaponOther()
    {
        weaponAudio.Stop();
        weaponAudio.clip = weaponOther;
        weaponAudio.Play();
    }

    public void playerAudio_weaponEnemy()
    {
        weaponAudio.Stop();
        weaponAudio.clip = weaponEnemy;
        weaponAudio.Play();
    }

    public void playerAudio_weaponBossShield()
    {
        weaponAudio.Stop();
        weaponAudio.clip = weaponBossShield;
        weaponAudio.Play();
    }
}
