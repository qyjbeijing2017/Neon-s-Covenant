using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimelineTrigger : MonoBehaviour
{

    [SerializeField] UnityEngine.Playables.PlayableDirector d;
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "MainCharacter")
        {
            d.Play();
        }
    }
}
