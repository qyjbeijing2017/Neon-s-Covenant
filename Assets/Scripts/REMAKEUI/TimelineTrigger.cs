using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimelineTrigger : MonoBehaviour
{

    [SerializeField] UnityEngine.Playables.PlayableDirector d;
    bool used 
     =false;
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "MainCharacter" && !used)
        {
            used = true;
            d.Play();
        }
    }
}
