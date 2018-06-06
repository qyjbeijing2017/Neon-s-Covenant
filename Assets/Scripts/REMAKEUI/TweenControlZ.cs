using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TweenControlZ : MonoBehaviour
{
    Tweener ft;
    Tweener merge;
    [SerializeField] float relativeMergeZ = -2.0f;
    [SerializeField] float mergeTime = 1f;
    [SerializeField] float relativeLoopZ = -1.5f;
    [SerializeField] float loopTime = 2.0f;
    void Start()
    {
        merge = transform.DOLocalMoveZ(relativeMergeZ, mergeTime);
        merge.OnComplete(StartTweener);
    }
    public void StartTweener()
    {
        ft = transform.DOLocalMoveZ(relativeLoopZ, loopTime).SetLoops(-1, LoopType.Yoyo);
    }
}
