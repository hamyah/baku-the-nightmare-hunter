using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private string gameSceneName = "MainScene";
    [SerializeField] private GameObject buttonsGroup;
    [SerializeField] private EventChannelSO settingsToggleEvent;


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
