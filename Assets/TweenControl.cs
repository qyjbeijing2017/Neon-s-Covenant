using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TweenControl : MonoBehaviour
{
    Tweener ft;
    Tweener merge;
    [SerializeField] float relativeMergeY = 1.0f;
    [SerializeField] float mergeTime = 1f;
    [SerializeField] float relativeLoopY = 0.5f;
    [SerializeField] float loopTime = 2.0f;
    void Start()
    {
        merge = transform.DOLocalMoveY(relativeMergeY, mergeTime);
        merge.OnComplete(StartTweener);
    }
    public void StartTweener()
    {
        ft = transform.DOLocalMoveY(relativeLoopY, loopTime).SetLoops(-1, LoopType.Yoyo);
    }
}
