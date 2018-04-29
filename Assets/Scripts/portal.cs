using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class portal : MonoBehaviour {
    [SerializeField] string s;
    private void OnTriggerEnter(Collider other)
    {
        SceneManager.LoadScene(s);
    }
}
