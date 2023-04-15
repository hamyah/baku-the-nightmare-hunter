using UnityEngine;
using UnityEngine.UI;


public class SpawnableObject : MonoBehaviour
{

    [SerializeField] private float lifetime;
    [SerializeField] private EventChannelSO lifetimeOverEventChanel;
    
    private float _timeElapsed;
    private bool _isActivated;
    private Image _image;
    private Button _button;
    
    void Start() {
        _image = GetComponent<Image>();
        _button = GetComponent<Button>();

        Deactivate();
    }

    void Update()
    {
        if (!_isActivated || lifetime == 0.0)
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

    public void Activate(float lifetime) {
        _isActivated = true;

        _timeElapsed = 0;
        SetLifetime(lifetime);

        _image.enabled = true;
        _button.enabled = true;
    }

    public bool IsActivated() {
        Debug.Log(_isActivated);
        return _isActivated;
    }

    public void Deactivate() {
        _isActivated = false;

        _image.enabled = false;
        _button.enabled = false;
    }

}
