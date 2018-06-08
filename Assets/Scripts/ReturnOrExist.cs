using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReturnOrExist : MonoBehaviour {
    [SerializeField]int sceneMenu;
    [SerializeField]int sceneRestart;
    
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void sceneRestartNow()
    {
        SceneManager.LoadScene(sceneRestart);
    }
    public void backToMain()
    {
        SceneManager.LoadScene(sceneMenu);
    }
    public void existNow()
    {
        Application.Quit();
    }
}
