using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CutScene : MonoBehaviour
{

    void Start() {
        LeanTween.scaleZ(gameObject, 1, 35).setOnComplete(Skip);
    }

    public void Skip() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }
}
