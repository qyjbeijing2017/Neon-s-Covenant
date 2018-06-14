using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BossShakeCamera : MonoBehaviour {
    DOTweenAnimation tweenAnimation;


	// Use this for initialization
	void Start () {
        ShakeCamera();
	}
	
	// Update is called once per frame
	void Update () {

	}

    public void ShakeCamera()
    {
        Camera.main.DOShakePosition(0.8f, new Vector3(0.3f, 0.3f, 0.5f));
    }
}
