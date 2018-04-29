using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioStart : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip clip;
    [SerializeField] [Range(0, 1)] float Volume;

    [SerializeField] bool again;

    // Use this for initialization
    void Start()
    {
        again = false;
        audioSource.loop = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "MainCharacter" && !again)
        {
            audio_start();
            again = true;
        }
    }

    public void audio_start()
    {
        audioSource.Stop();
        audioSource.clip = clip;
        audioSource.Play();
    }
}
