using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMaterialSwitcher : MonoBehaviour
{
    [SerializeField] SkinnedMeshRenderer bossHead;
    [SerializeField] SkinnedMeshRenderer bossBody;
    [SerializeField] SkinnedMeshRenderer bossSkirt;
    public NSC_Color t;
    [SerializeField] Material matRed;
    [SerializeField] Material matCyan;
    [SerializeField] Material matNormal;
    void Update()
    {
        switch (t.m_colorType.GetHashCode())
        {
            case 0://白色~
                {
                    bossHead.material = matNormal;
                    bossBody.material = matNormal;
                    bossSkirt.material = matNormal;
                }
                break;
            case 1://红色~
                {
                    bossHead.material = matRed;
                    bossBody.material = matRed;
                    bossSkirt.material = matRed;
                }
                break;
            case 2://青色~
                {
                    bossHead.material = matCyan;
                    bossBody.material = matCyan;
                    bossSkirt.material = matCyan;
                }
                break;
        }
    }
}
