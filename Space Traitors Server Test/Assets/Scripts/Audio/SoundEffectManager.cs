using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffectManager : MonoBehaviour
{
    private AudioSource audioSource;

    public AudioClip specChallengeFail;
    public AudioClip specChallengeSuccess;
    public AudioClip failedChoice;

    public Server server;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        
    }
}
