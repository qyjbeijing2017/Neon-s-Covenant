using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class black : MonoBehaviour
{
    [SerializeField] Image blackNow;
    [SerializeField] Boss_new boss;
    [SerializeField] Player_new player;
    [SerializeField] GameObject Black;

    // Use this for initialization
    void Start()
    {
        Black.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        if (player.HP <= 0)
        {
            Black.SetActive(true);
        }

        if (boss && boss.HP <= 0)
        {
            Black.SetActive(true);
        }

    }


}
