/*
 * File: TweenControlY.cs
 * Project: REMAKEUI
 * File Created: Thursday, 7th June 2018 1:05:41 am
 * Author: shpkng (shpkng@gmail.com)
 * -----
 * Last Modified: Saturday, 16th June 2018 5:22:12 am
 * Modified By: shpkng (shpkng@gmail.com>)
 * -----
 * loving the lovely sunshine in autumn.♥
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(Floating))]
public class TweenControlY : MonoBehaviour
{

    Tweener ft;
    Tweener merge;
    [SerializeField] float relativeMergeY = 2.0f;
    [SerializeField] float relativeScale;
    [SerializeField] float mergeTime = 1f;
    Floating flt;
    void Start()
    {
        flt = GetComponent<Floating>();
        merge = transform.DOLocalMoveY(transform.position.y + relativeMergeY, mergeTime);
        merge.OnComplete(StartTweener);
    }
    public void StartTweener()
    {
        flt.enabled = true;
    }
}
