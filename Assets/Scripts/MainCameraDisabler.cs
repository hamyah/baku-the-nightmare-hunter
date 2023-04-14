using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCameraDisabler : MonoBehaviour
{
    void Awake() {
        if(Camera.main.gameObject != gameObject) {
            gameObject.SetActive(false);
        }
    }
}
