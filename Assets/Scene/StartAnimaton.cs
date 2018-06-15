using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.Playables;

public class StartAnimaton : MonoBehaviour {
	[SerializeField] public PlayableDirector playableDirector;
	[SerializeField] public TimelineAsset Timeline1;
	[SerializeField] public TimelineAsset Timeline2;

	float timer;
    
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime;
        
		if(timer>= Timeline1.duration){
			playableDirector.playableAsset = Timeline2;
			playableDirector.Play();

			enabled = false;
		}

	}
}
