using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class portal : MonoBehaviour {
    [SerializeField] int nextSceneNo;
    private void OnTriggerEnter(Collider other)
    {
        SceneManager.LoadScene(nextSceneNo);
    }
}
