using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseAudioManager : MonoBehaviour
{
    public List<AudioClip> audioClips = new List<AudioClip>();
    public float volumeMaxThreshold = 1f;
    protected void PlaySound(AudioSource source, AudioClip clip) {
        source.volume = calculateVolume() * volumeMaxThreshold;
        source.clip = clip;
        source.Play();
    }

    static private float calculateVolume() {
        return MusicManager.GetVolume();
    }
}
