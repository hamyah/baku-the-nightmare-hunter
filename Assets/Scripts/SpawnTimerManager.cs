using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SpawnTimerManager : MonoBehaviour
{
    public static UnityEvent onSpawnObject;

    public class SpawnObjectEvent : UnityEvent<int, > {
        
    }

    void Start() {
        if(onSpawnObject == null) {
            onSpawnObject
        }
    }
}
