using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIPause : MonoBehaviour
{
    [SerializeField] GameObject Pause;
    [SerializeField] KeyCode PauseKey;
    bool pauseNow;

    // Use this for initialization
    void Start()
    {
        Pause.SetActive(false);
        pauseNow = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(PauseKey))
        {

            PauseSet(!pauseNow);
        }

    }

    public void PauseSet(bool Stop)
    {
        if (!Stop)
        {
            Time.timeScale = 1;
            Pause.SetActive(false);
            pauseNow = false;
        }
        else
        {
            Time.timeScale = 0;
            Pause.SetActive(true);
            pauseNow = true;
        }
    }

    public void LoadSave()
    {
        FindObjectOfType<Player_save>().ReadGame();
        PauseSet(false);
    }


    public void LoadScene(int SceneNub)
    {
        SceneManager.LoadScene(SceneNub);
        Time.timeScale = 1;
    }


    public void QuitGame()
    {
        Application.Quit();
    }

}
