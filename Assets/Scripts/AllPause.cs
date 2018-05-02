using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllPause : MonoBehaviour
{
    bool pause;
    [SerializeField] KeyCode pauseKey;
    [SerializeField] AudioSource theOtherSaid;
    [SerializeField] GameObject 毛玻璃;


    // Use this for initialization
    void Start()
    {
        pause = false;
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(pauseKey))
        {
            pauseAll(!pause);
            pause = !pause;


        }
        if (pause)
        {
            毛玻璃.SetActive(true);
        }
        else
        {
            毛玻璃.SetActive(false);
        }
    }

    public void pauseMove(bool pauseNow)
    {
        if (!pauseNow)
        {
            Time.timeScale = 1;
            if (FindObjectOfType<Player_new>())
            {
                FindObjectOfType<Player_new>().player_start();

            }
        }
        else
        {
            Time.timeScale = 0;
            if (FindObjectOfType<Player_new>())
            {
                FindObjectOfType<Player_new>().player_stopImmediately();
            }
        }
    }

    public void pauseAll(bool pauseNow)
    {
        if (!pauseNow)
        {
            Time.timeScale = 1;
            if (FindObjectOfType<Player_new>())
            {
                FindObjectOfType<Player_new>().player_start();
                theOtherSaid.UnPause();

            }
        }
        else
        {
            Time.timeScale = 0;
            if (FindObjectOfType<Player_new>())
            {
                FindObjectOfType<Player_new>().player_stopImmediately();
                theOtherSaid.Pause();
            }
        }
    }



}
