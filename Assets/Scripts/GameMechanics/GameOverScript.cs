using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScript : MonoBehaviour
{
    void Awake() {
        GetComponent<SimultaniousAudioManager>().PlaySound();
    }
    void Update() {
        if(Input.GetKeyDown(KeyCode.Space)) {
            SceneManager.LoadScene(0);
        }
    }
}
