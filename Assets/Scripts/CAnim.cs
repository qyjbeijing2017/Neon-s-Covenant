using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// CAnim类依赖Animator
/// </summary>
[RequireComponent(typeof(Animator))]
public class CAnim : MonoBehaviour
{
	protected Animator animator;
	protected CProperty property;
	protected CBehaviour behaviour;

	protected void Awake()
	{
		animator = GetComponent<Animator>();
		property = GetComponent<CProperty>();
		behaviour = GetComponent<CBehaviour>();
	}

	public virtual void SetMovingAnim() { }
	public virtual void SetMovingAnim(float speed) { animator.SetFloat("speed", speed); }
	public virtual void SetMovingAnim(float speed, bool isRunning)
	{
		animator.SetFloat("speed", speed);
		animator.SetBool("running", isRunning);
	}
	public virtual void SetMovingAnim(bool isMoving){ animator.SetBool("moving", isMoving); }
	public virtual void SetLaserLooping(bool isLooping){ animator.SetBool("laser", isLooping); }
	public virtual void PlayAnim(string clip)
	{
		animator.Play   (clip,0,0);
	}

	public virtual void PlayAnim(string clip,float time)
	{
		animator.Play(clip, 0, 0);
	}
}
