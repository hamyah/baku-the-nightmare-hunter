using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsMenu : MonoBehaviour
{
    [SerializeField] private GameObject settingsGroup;
    [SerializeField] private EventChannelSO settingsToggleEvent;


    private void OnEnable()
    {
        settingsToggleEvent.OnEventRaised += ToggleSettingsVisibility;
    }
    
    private void OnDisable()
    {
        settingsToggleEvent.OnEventRaised -= ToggleSettingsVisibility;
    }

    void Start()
    {
        ToggleSettingsVisibility();
    }
    

    public void ToggleSettingsVisibility()
    {
        settingsGroup.SetActive(!settingsGroup.activeSelf);
    }
}
