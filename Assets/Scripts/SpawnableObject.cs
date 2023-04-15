using UnityEngine;


public class SpawnableObject : MonoBehaviour
{

    [SerializeField] private float lifetime;
    [SerializeField] private EventChannelSO lifetimeOverEventChanel;
    
    private float _timeElapsed;
    

    void Update()
    {
        if (lifetime == 0.0)
            return;
        
        if (_timeElapsed >= lifetime)
        {
            lifetimeOverEventChanel.RaiseEvent();
            Destroy(this);
        }

        _timeElapsed += Time.deltaTime;
    }


    public void SetLifetime(float time)
    {
        lifetime = time;
    }

}
