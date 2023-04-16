using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class CutScene : MonoBehaviour
{
    public VideoPlayer videoPlayer;

    void Start() {
        LeanTween.scaleZ(gameObject, 1, 35).setOnComplete(Skip);

        videoPlayer.url = System.IO.Path.Combine (Application.streamingAssetsPath,"Intro.mp4");
        videoPlayer.Play();
    }

    public void Skip() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }
}
