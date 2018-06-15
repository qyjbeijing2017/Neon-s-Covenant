/*
 * File: CanvasShift.cs
 * Project: REMAKEUI
 * File Created: Tuesday, 5th June 2018 10:49:58 pm
 * Author: shpkng (shpkng@gmail.com)
 * -----
 * Last Modified: Saturday, 16th June 2018 5:21:12 am
 * Modified By: shpkng (shpkng@gmail.com>)
 * -----
 * loving the lovely sunshine in autumn.♥
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasShift : MonoBehaviour
{
    [SerializeField] float ratio;
    // Update is called once per frame
    void Update()
    {
        Vector3 move3 = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")) * ratio;
        if (move3 != Vector3.zero)
        {
            transform.position -= move3;

        }
    }
}
