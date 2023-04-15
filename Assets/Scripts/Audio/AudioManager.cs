using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public List<AudioClip> audioClips = new List<AudioClip>();
    public float volumeMaxThreshold = 1f;
    
    [SerializeField]
    private AudioSource audioSource;

    public void PlaySound() {
        var clip = audioClips[Random.Range(0, audioClips.Count)];
        PlaySound(audioSource, clip);
    }

    private void PlaySound(AudioSource source, AudioClip clip) {
        audioSource.volume = calculateVolume() * volumeMaxThreshold;
        audioSource.clip = clip;
        audioSource.Play();
    }

    static private float calculateVolume() {
        return MusicManager.GetVolume();
    }
}
