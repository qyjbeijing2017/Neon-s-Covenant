using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class returnMenu : MonoBehaviour {

    [SerializeField] int sceneID;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void loadSence()
    {
        SceneManager.LoadScene(sceneID);
    } 
}
