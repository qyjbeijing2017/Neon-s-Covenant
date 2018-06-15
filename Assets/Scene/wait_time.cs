using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class wait_time : MonoBehaviour {

    [SerializeField] int sceneID;
    [SerializeField] VideoPlayer videoPlayer;
    float timer = 0.0f;
	// Use this for initialization
	void Start () {

        
	}

	
	// Update is called once per frame
	void Update () {
        if (Input.anyKeyDown)
        {
            SceneManager.LoadScene(sceneID);
        }


        timer += Time.deltaTime;

        if(videoPlayer.frameCount / videoPlayer.frameRate < timer)
        {
            SceneManager.LoadScene(sceneID);

        }
	}
}
