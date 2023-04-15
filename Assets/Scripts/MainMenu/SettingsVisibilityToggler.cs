using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsVisibilityToggler : MonoBehaviour
{
    [SerializeField] private EventChannelSO settingsToggleEvent;
    
    public void OnToggleSettingsVisibility()
    {
        settingsToggleEvent.RaiseEvent();
    }
}
