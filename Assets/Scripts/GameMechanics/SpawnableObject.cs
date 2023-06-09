using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;


public class SpawnableObject : MonoBehaviour
{
    public Sprite activatedSprite;
    public Sprite deactivatedSprite;
    public float maxSpawnRateVFX = 10;

    public float startTimeVFX = 15;
    public float topTimeVFX = 7;


    [SerializeField] private float lifetime;
    [SerializeField] protected IntEventChannelSO lifetimeOverEventChanel;
    
    private float _timeElapsed;
    protected bool _isActivated;
    private Image _image;
    [SerializeField] private GameObject _button;
    public ParticleSystem _warningVFX;

    public GameObject jumpScare;

    private bool gameOver = false;
    
    public void Start() {
        _image = GetComponent<Image>();
        this._warningVFX = GetComponentInChildren<ParticleSystem>();

        Deactivate();
    }

    void Update()
    {
        if (!_isActivated || lifetime == 0.0)
            return;
        
        if (!gameOver && _timeElapsed >= lifetime)
        {
            OnTimerTriggered();
        }

        if(_timeElapsed > startTimeVFX) {
            var emission = _warningVFX.emission;
            emission.rateOverTime = Mathf.Lerp(0, maxSpawnRateVFX, (_timeElapsed-startTimeVFX)/(lifetime-startTimeVFX-topTimeVFX));
            
            //Mathf.Clamp((_timeElapsed-startTimeVFX)/(lifetime-topTimeVFX), 0, 1) * maxSpawnRateVFX;

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
        _button.SetActive(true);
    }

    public bool IsActivated() {
        return _isActivated;
    }

    public void Deactivate() {
        Debug.Log("Deactivate");
        _isActivated = false;

        _image.sprite = deactivatedSprite;
        _button.SetActive(false);
        var emission = _warningVFX.emission;
        emission.rateOverTime = 0;
    }

    public virtual void OnTimerTriggered() {
        gameOver = true;
        int roomId = transform.parent.parent.GetSiblingIndex();

        // FIXME: raise the event with the id 

        lifetimeOverEventChanel.RaiseEvent(roomId);

        LeanTween.move(gameObject, Vector3.zero, 0.5f).setDelay(0.5f).setIgnoreTimeScale(true);
        LeanTween.scale(gameObject, new Vector3(5, 5, 1), 0.5f).setDelay(0.5f).setIgnoreTimeScale(true);
        LeanTween.scaleZ(gameObject, 1, 2f).setIgnoreTimeScale(true).setOnComplete(SpawnJumpscare);

        //Destroy(this);
    }

    void SpawnJumpscare() {
        Instantiate(jumpScare);
    }

}
