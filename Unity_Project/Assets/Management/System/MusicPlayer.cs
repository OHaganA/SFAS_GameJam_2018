using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicPlayer : MonoBehaviour
{
    public AudioClip[] levelMusicChange;

    private AudioSource audioSource;

    static MusicPlayer instance = null;
    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            print("Duplicate music player self-destructing!");
        }
        else
        {
            DontDestroyOnLoad(gameObject);
            audioSource = GetComponent<AudioSource>();
        }
        
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        AudioClip audioClip = levelMusicChange[scene.buildIndex];
        if (!audioClip) return;
        audioSource.clip = audioClip;
        audioSource.loop = true;
        audioSource.Play();
        Debug.Log("Playing Clip " + audioClip);
    }

    public void changeVolume(float volume)
    {
        audioSource.volume = volume;
    }
}