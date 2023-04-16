using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private string gameSceneName = "CutScene";
    [SerializeField] private GameObject buttonsGroup;
    [SerializeField] private EventChannelSO settingsToggleEvent;

    void Start() {
        Time.timeScale = 1;
    }


    private void OnEnable()
    {
        settingsToggleEvent.OnEventRaised += ToggleMenuVisibility;
    }    
    
    private void OnDisable()
    {
        settingsToggleEvent.OnEventRaised += ToggleMenuVisibility;
    }

    public void OnPlayPressed()
    {
        SceneManager.LoadScene(gameSceneName);
    }

    public void ToggleMenuVisibility()
    {
        buttonsGroup.SetActive(!buttonsGroup.activeSelf);
    }

    public void OnExitPressed()
    {
        Application.Quit();
    }
}
