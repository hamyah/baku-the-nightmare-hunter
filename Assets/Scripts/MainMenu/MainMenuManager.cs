using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private string gameSceneName = "MainScene";
    

    public void OnPlayPressed()
    {
        SceneManager.LoadScene(gameSceneName);
    }

    public void OnExitPressed()
    {
        Application.Quit();
    }
}
