using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class NSC_Color
{

    // Use this for initialization
    
    public enum colorType
    {
        white = 0,
        red = 1,
        cyan = 2,
        black = 3
    }
    [Tooltip("颜色的值")]
    public float colorValue;
    [Tooltip("颜色的类型")]
    public colorType m_colorType;



/// <summary>
/// 用于计算a、b是否同类型，白色、黑色也计算在内。
/// </summary>
/// <param name="a"></param>
/// <param name="b"></param>
/// <returns></returns>
    public static bool colorSame(NSC_Color a, NSC_Color b)
    {
        if(a.m_colorType == b.m_colorType)
        {
            return true;
        }
        return false;
    }

/// <summary>
/// 只用于计算a、b分别为青、红，或者红、青的状况。
/// </summary>
/// <param name="a"></param>
/// <param name="b"></param>
/// <returns></returns>

    public static bool colorContrary(NSC_Color a, NSC_Color b)
    {
        if(a.m_colorType == colorType.red && b.m_colorType == colorType.cyan)
        {
            return true;
        }
        else if(a.m_colorType == colorType.cyan && b.m_colorType == colorType.red)
        {
            return true;
        }
        else
        {
            return false;
        }

    }


}
