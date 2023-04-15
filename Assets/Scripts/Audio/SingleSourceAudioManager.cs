using UnityEngine;

public class SingleSourceAudioManager : BaseAudioManager
{    
    [SerializeField]
    private AudioSource audioSource = null;

    public void PlaySound() {
        var clip = audioClips[Random.Range(0, audioClips.Count)];
        PlaySound(audioSource, clip);
    }
}
