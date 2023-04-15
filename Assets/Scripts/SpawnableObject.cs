using UnityEngine;
using UnityEngine.UI;


public class SpawnableObject : MonoBehaviour
{
    public Sprite activatedSprite;
    public Sprite deactivatedSprite;


    [SerializeField] private float lifetime;
    [SerializeField] protected EventChannelSO lifetimeOverEventChanel;
    
    private float _timeElapsed;
    protected bool _isActivated;
    private Image _image;
    private Button _button;
    
    public void Start() {
        _image = GetComponent<Image>();
        _button = GetComponentInChildren<Button>();

        Deactivate();
    }

    void Update()
    {
        if (!_isActivated || lifetime == 0.0)
            return;
        
        if (_timeElapsed >= lifetime)
        {
            OnTimerTriggered();
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

        _image.sprite = activatedSprite;
        _button.enabled = true;
    }

    public bool IsActivated() {
        return _isActivated;
    }

    public void Deactivate() {
        _isActivated = false;

        _image.sprite = deactivatedSprite;
        _button.enabled = false;
    }

    public virtual void OnTimerTriggered() {
        lifetimeOverEventChanel.RaiseEvent();
        Destroy(this);
    }

}
