using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimultaniousAudioManager : BaseAudioManager
{
    public List<AudioSource> audioSources = new List<AudioSource>();

    public void PlaySound() {
        int index = 0;
        while (index < audioClips.Count && index < audioSources.Count) {
            PlaySound(audioSources[index], audioClips[index]);
            index++;
        }
    }
}
