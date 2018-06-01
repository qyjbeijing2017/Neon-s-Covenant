using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(AudioSource))]
public class AudioPlay : MonoBehaviour {
    AudioAdd[] audios;
    AudioSource audioSource;

    public void playAudio(int audioNub)
    {
        audioSource.Stop();
        audioSource.clip = audios[audioNub].clip;
        audioSource.volume = audios[audioNub].volume;
        audioSource.Play();
    }
    public void playPause()
    {
        audioSource.Pause();
    }
    public void playContinue()
    {
        audioSource.Play();
    }
    public void playStop()
    {
        audioSource.Stop();
    }

}
