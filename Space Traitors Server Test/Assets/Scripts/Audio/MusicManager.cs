using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour
{
    private AudioSource audioSource;

    public AudioClip menuMusic;
    public AudioClip gameMusic;

    private void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;
        

        if (sceneName == "server")
        {
            ChangeMusicClip(gameMusic);
        }
        else if (sceneName == "Character Select" || sceneName == "LobbyTest")
        {
            ChangeMusicClip(menuMusic);
        }
    }

    private void StopMusic()
    {
        audioSource.Stop();
    }

    private void ChangeMusicClip(AudioClip audioClip)
    {
        
        if (!audioSource.isPlaying || audioSource.clip != audioClip)
        {
            audioSource.clip = audioClip;
            audioSource.Play();
        }
    }
}
