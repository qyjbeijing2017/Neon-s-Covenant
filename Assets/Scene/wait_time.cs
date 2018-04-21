using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class wait_time : MonoBehaviour {

	// Use this for initialization
	void Start () {
        StartCoroutine(wait());

	}
    IEnumerator wait()
    {
        yield return new WaitForSeconds(88);
        SceneManager.LoadScene(1);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
