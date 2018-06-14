using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(FloatingZ))]
public class TweenControlZ : MonoBehaviour
{
    Tweener ft;
    Tweener merge;
    [SerializeField] float relativeMergeZ = 2.0f;
    [SerializeField] float relativeScale;
    [SerializeField] float mergeTime = 1f;
	FloatingZ z;
    void Start()
    {
        z= GetComponent<FloatingZ>();
        merge = transform.DOLocalMoveZ(transform.position.z + relativeMergeZ, mergeTime);
        merge.OnComplete(StartTweener);
    }
    public void StartTweener()
    {
       z.enabled = true;
    }
}
