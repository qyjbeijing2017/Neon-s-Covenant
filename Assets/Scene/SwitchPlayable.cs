using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class SwitchPlayable : MonoBehaviour {
[SerializeField] float triggerTime = 0;
[SerializeField]PlayableAsset playable;
float timer = 0;
	
	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime;
		if(timer>=triggerTime)
		{
			PlayableDirector p = GetComponent<PlayableDirector>();
			p.playableAsset = playable;
			p.Play();
		}
	}
}
