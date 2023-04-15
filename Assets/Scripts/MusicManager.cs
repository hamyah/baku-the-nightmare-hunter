using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour
{
    public AudioSource IntroMusicSource;
    public AudioSource LoopMusicSource; 
    public float musicVolume = 0.15f;

    public string MainSceneName;
    public string EndSceneName;

    public static MusicManager Instance = null;

    private void Awake() {
        if (Instance == null) {
            Instance = this;
        } else {
            Destroy(gameObject);
        }

        SetMusicVolume(musicVolume);

        DontDestroyOnLoad(gameObject);
    }

    void OnEnable() {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
        string currentSceneName = SceneManager.GetActiveScene().name;

        if (currentSceneName == MainSceneName && (!IntroMusicSource.isPlaying || LoopMusicSource.isPlaying)) {
            LoopMusicSource.PlayScheduled((float)AudioSettings.dspTime + IntroMusicSource.clip.length);
            StartIntroMusic();
        }
    }

    public void SetMusicVolume(float volume) {
        IntroMusicSource.volume = volume;
        LoopMusicSource.volume = volume;
    }

    private void StartIntroMusic() {
        LoopMusicSource.loop = true;
        IntroMusicSource.PlayOneShot(IntroMusicSource.clip);
    }
}
