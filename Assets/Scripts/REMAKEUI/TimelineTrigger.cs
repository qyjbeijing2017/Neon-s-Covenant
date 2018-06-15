/*
 * File: TimelineTrigger.cs
 * Project: REMAKEUI
 * File Created: Tuesday, 5th June 2018 10:50:07 pm
 * Author: shpkng (shpkng@gmail.com)
 * -----
 * Last Modified: Saturday, 16th June 2018 5:21:46 am
 * Modified By: shpkng (shpkng@gmail.com>)
 * -----
 * loving the lovely sunshine in autumn.♥
 */


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
