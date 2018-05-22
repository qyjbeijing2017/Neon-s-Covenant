using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NSC_Color : MonoBehaviour
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


    //public static NSC_Color operator +(NSC_Color a, NSC_Color b)
    //{
    //    NSC_Color newColor = new NSC_Color();
    //    if (a.m_colorType == b.m_colorType)
    //    {
    //        newColor.m_colorType = a.m_colorType;
    //        newColor.colorValue = a.colorValue + b.colorValue;
    //    }
    //    else
    //    {
    //        if (((a.m_colorType == colorType.black && b.m_colorType == colorType.white) ||
    //            (a.m_colorType == colorType.white && b.m_colorType == colorType.black)) ||
    //            ((a.m_colorType == colorType.red && b.m_colorType == colorType.cyan) ||
    //            (a.m_colorType == colorType.cyan && b.m_colorType == colorType.red)))
    //        {
    //            if (a.colorValue >= b.colorValue)
    //            {
    //                newColor.m_colorType = a.m_colorType;
    //                newColor.colorValue = a.colorValue - b.colorValue;
    //            }
    //            else
    //            {
    //                newColor.m_colorType = b.m_colorType;
    //                newColor.colorValue = 0;
    //            }
    //        }
    //        else
    //        {
    //            if (a.colorValue >= b.colorValue)
    //            {

    //            }
    //        }
    //    }
    //    return newColor;
    //}


    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
