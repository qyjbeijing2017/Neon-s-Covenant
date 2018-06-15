using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioAndText : MonoBehaviour {
    [SerializeField] AudioPlay audioPlay;
    [SerializeField] Text text;
    [SerializeField] string showText;
    public int Num;
    [SerializeField] float showTime;
    [SerializeField] Player_save player_Save;

	// Use this for initialization
	void Start () {
        text.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Player>())
        {
            audioPlay.playAudio(Num);
            startText();
        }
    }

    void startText()
    {
        text.text = showText;
        text.GetComponent<TestEnd>().testShowTime = showTime;
        text.GetComponent<TestEnd>().timer = 0;
        text.enabled = true;
        gameObject.SetActive(false);
    }
}
