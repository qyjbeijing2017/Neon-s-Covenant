using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_audio : MonoBehaviour {
    enum BossAudio
    {
        empty = 1,
        powerful
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

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
