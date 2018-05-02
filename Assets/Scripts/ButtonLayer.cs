using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonLayer : MonoBehaviour
{

	public void NextScene()
	{
		SceneManager.LoadScene("video");
	}

	public void Exit()
	{
		Application.Quit();
	}
}
