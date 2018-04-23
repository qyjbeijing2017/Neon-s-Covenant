using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_audio : MonoBehaviour
{
    enum BossAudio
    {
        empty = 1,
        powerful = 2,
        weapon = 3,
        laser = 4,
        range = 5,
    }

    [SerializeField] AudioClip boss_weaponEmpty;
    [Range(0, 1)] [SerializeField] float emptyVolume;

    [SerializeField] AudioClip boss_weaponPowerfulEmpty;
    [Range(0, 1)] [SerializeField] float powerfulEmptyVolume;

    [SerializeField] AudioClip boss_weaponPlayer;
    [Range(0, 1)] [SerializeField] float playerVolume;

    [SerializeField] AudioClip boss_laser;
    [Range(0, 1)] [SerializeField] float laserVolume;

    [SerializeField] AudioClip boss_range;
    [Range(0, 1)] [SerializeField] float rangeVolume;

    [SerializeField] AudioSource bossAudio;

    //Use this for initialization

    void Start()
    {


    }

    //Update is called once per frame

    void Update()
    {

    }

    public void bossAudio_play(int a)
    {
        bossAudio.loop = false;
        bossAudio.Stop();
        switch (a)
        {
            case 1:

                bossAudio.clip = boss_weaponEmpty;
                bossAudio.volume = emptyVolume;

                break;
            case 2:

                bossAudio.clip = boss_weaponPowerfulEmpty;
                bossAudio.volume = powerfulEmptyVolume;

                break;
            case 3:

                bossAudio.clip = boss_weaponPlayer;
                bossAudio.volume = playerVolume;

                break;
            case 4:

                bossAudio.clip = boss_laser;
                bossAudio.loop = true;
                bossAudio.volume = laserVolume;

                break;
            case 5:

                bossAudio.clip = boss_range;
                bossAudio.volume = rangeVolume;

                break;


        }
        bossAudio.Play();
    }

    public void bossAudio_stop()
    {
        bossAudio.Stop();
    }

}
