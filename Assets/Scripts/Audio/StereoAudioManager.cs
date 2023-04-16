using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StereoAudioManager : BaseAudioManager
{
    [SerializeField]
    private AudioSource leftAudioSource;
    [SerializeField]
    private AudioSource rightAudioSource;

    public enum AudioTargetPosition {
        RIGHT,
        LEFT
    }

    public void PlaySound(AudioTargetPosition targetPosition) {
        var clip = audioClips[Random.Range(0, audioClips.Count)];

        switch(targetPosition) {
            case AudioTargetPosition.RIGHT:
                PlaySound(rightAudioSource, clip);
                break;
            case AudioTargetPosition.LEFT:
                PlaySound(leftAudioSource, clip);
                break;
            default:
                throw new System.Exception();
        }
    }
}
