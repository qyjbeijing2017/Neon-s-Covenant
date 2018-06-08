using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class wait_time : MonoBehaviour {

    [SerializeField]int sceneID;
	// Use this for initialization
	void Start () {
        StartCoroutine(wait());

	}
    IEnumerator wait()
    {
        yield return new WaitForSeconds(88);
        SceneManager.LoadScene(sceneID);
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.anyKeyDown)
        {
            SceneManager.LoadScene(sceneID);
        }
		
	}
}
