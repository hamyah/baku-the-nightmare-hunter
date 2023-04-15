using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerAudioManager : MonoBehaviour
{
    public List<AudioClip> firstObjectSpawnClips = new List<AudioClip>();
    public float firstObjectSpawnVolume = 0.3f;
    
    [SerializeField]
    private AudioSource audioSource;

    public enum AudioTargetPosition {
        RIGHT,
        LEFT
    }

    public void PlayFirstObjectSpawn() {
        Debug.Log("Play Anomaly");
        var clip = firstObjectSpawnClips[Random.Range(0, firstObjectSpawnClips.Count)];
        
        audioSource.volume = firstObjectSpawnVolume;
        audioSource.clip = clip;
        audioSource.Play();
    } 
}
