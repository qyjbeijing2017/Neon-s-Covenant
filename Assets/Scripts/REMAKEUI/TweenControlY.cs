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
