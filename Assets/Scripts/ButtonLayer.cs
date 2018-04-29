using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonLayer : MonoBehaviour
{

	public void NextScene()
	{
		Debug.LogError("Yo");
		SceneManager.LoadScene("video");
	}

	public void Exit()
	{
		Application.Quit();
	}
}
