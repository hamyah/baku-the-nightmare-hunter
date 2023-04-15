using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManagerAudioManager : MonoBehaviour
{
    public List<AudioClip> changeRoomAudioClips = new List<AudioClip>();
    public float changeRoomVolume = 0.3f;
    
    [SerializeField]
    private AudioSource leftAudioSource;
    [SerializeField]
    private AudioSource rightAudioSource;

    public enum AudioTargetPosition {
        RIGHT,
        LEFT
    }

    public void PlayChangeRoomSound(AudioTargetPosition targetPosition) {
        Debug.Log("Play Anomaly");
        var clip = changeRoomAudioClips[Random.Range(0, changeRoomAudioClips.Count)];

        switch(targetPosition) {
            case AudioTargetPosition.RIGHT:
                rightAudioSource.volume = changeRoomVolume;
                rightAudioSource.clip = clip;
                rightAudioSource.Play();
                break;
            case AudioTargetPosition.LEFT:
                leftAudioSource.volume = changeRoomVolume;
                leftAudioSource.clip = clip;
                leftAudioSource.Play();
                break;
            default:
                throw new System.Exception();
        }
    } 
}
