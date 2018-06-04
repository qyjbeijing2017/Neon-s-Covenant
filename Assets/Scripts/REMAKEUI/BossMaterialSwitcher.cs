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
