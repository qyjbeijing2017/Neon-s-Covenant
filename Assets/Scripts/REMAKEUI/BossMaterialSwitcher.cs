/*
 * File: BossMaterialSwitcher.cs
 * Project: REMAKEUI
 * File Created: Tuesday, 5th June 2018 10:50:01 pm
 * Author: shpkng (shpkng@gmail.com)
 * -----
 * Last Modified: Saturday, 16th June 2018 5:21:06 am
 * Modified By: shpkng (shpkng@gmail.com>)
 * -----
 * loving the lovely sunshine in autumn.♥
 */


//这个代码是用来控制boss的材质的

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMaterialSwitcher : MonoBehaviour
{
    [SerializeField] SkinnedMeshRenderer[] targetMesh;

    NSC_Color t;
    [SerializeField] Material matRed;
    [SerializeField] Material matCyan;
    [SerializeField] Material matNormal;


    private void Start()
    {
        t = GetComponent<NSC_Character>().power;
    }

    void Update()
    {
        switch (t.m_colorType.GetHashCode())
        {
            case 0://白色~
                {
                    for(int i = 0; i < targetMesh.Length; i++)
                    {
                        targetMesh[i].material = matNormal;
                    }
                }
                break;
            case 1://红色~
                {
                    for (int i = 0; i < targetMesh.Length; i++)
                    {
                        targetMesh[i].material =matRed;
                    }
                }
                break;
            case 2://青色~
                {
                    for (int i = 0; i < targetMesh.Length; i++)
                    {
                        targetMesh[i].material = matCyan;
                    }
                }
                break;
        }
    }
}
