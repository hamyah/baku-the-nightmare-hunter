using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnomalyAudioManager : MonoBehaviour
{

    public List<AudioClip> anomaly_found_clips = new List<AudioClip>();
    
    [SerializeField]
    private AudioSource anomaly_audio_source;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayFindAnomaly() {
        Debug.Log("Play Anomaly");
        var clip = anomaly_found_clips[Random.Range(0, anomaly_found_clips.Count)];
        anomaly_audio_source.clip = clip;
        anomaly_audio_source.Play();
    } 
}
