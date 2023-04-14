using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SpawnableObject : MonoBehaviour
{

    [SerializeField] private float lifetime;
    [SerializeField] private UnityEvent onLifetimeOver;

    private float _timeElapsed;


    void Update()
    {
        if (lifetime == 0.0)
            return;
        
        if (_timeElapsed >= lifetime)
        {
            onLifetimeOver.Invoke();
            return;
        }

        _timeElapsed += Time.deltaTime;
    }


    public void SetLifetime(float time)
    {
        lifetime = time;
    }

}
