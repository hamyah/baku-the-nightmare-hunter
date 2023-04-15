using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour
{
    public AudioSource IntroMusicSource;
    public AudioSource LoopMusicSource; 
    static public float volume = 1f;
    public string MainSceneName;
    public static MusicManager Instance = null;

    private void Awake() {
        if (Instance == null) {
            Instance = this;
        } else {
            Destroy(gameObject);
        }

        SetMusicVolume(volume);

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
        MusicManager.volume = volume;
        IntroMusicSource.volume = volume;
        LoopMusicSource.volume = volume;
    }

    private void StartIntroMusic() {
        LoopMusicSource.loop = true;
        IntroMusicSource.PlayOneShot(IntroMusicSource.clip);
    }

    static public float GetVolume() {
        return volume;
    }
}
