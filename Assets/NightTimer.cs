using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NightTimer : MonoBehaviour
{
    public GameObject gameOver;
    public float totalTime;
    private float startTime;
    private bool finished;

    void Start() {
        startTime = Time.time;
        finished = false;
        Time.timeScale = 1;
    }

    void Update() {
        if(!finished && totalTime <= Time.time - startTime) {
            // timer expired
            Instantiate(gameOver);
            finished = true;
            Time.timeScale = 0;
        }
    }
}
