﻿/*
 * File: Floating.cs
 * Project: REMAKEUI
 * File Created: Friday, 15th June 2018 2:42:38 am
 * Author: shpkng (shpkng@gmail.com)
 * -----
 * Last Modified: Saturday, 16th June 2018 5:21:21 am
 * Modified By: shpkng (shpkng@gmail.com>)
 * -----
 * loving the lovely sunshine in autumn.♥
 */

using UnityEngine;  
using System.Collections;  
  
public class Floating : MonoBehaviour {  
  
    float radian = 0; // 弧度    
    float perRadian = 0.03f; // 每次变化的弧度   上下浮动  
    float radius = 0.1f; // 半径    
    Vector3 oldPos; // 开始时候的位置坐标    
    // Use this for initialization    
    void Start()  
    {  
        oldPos = transform.position; // 将最初的位置保存到oldPos    
    }  
  
    // Update is called once per frame    
    void Update()  
    {  
        radian += perRadian; // 弧度每次加0.03    
        float dy = Mathf.Sin(radian) * radius; // dy定义的是针对y轴的变量，也可以使用sin，找到一个适合的值就可以    
        transform.position = oldPos + new Vector3(0, dy, 0);  
    }    
}  