using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager : MonoBehaviour
{
    private AudioSource audioSource;

    public AudioClip specChallengeFail;
    public AudioClip specChallengeSuccess;
    public AudioClip failedChoice;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayFailedChoice()
    {
        PlaySoundEffect(failedChoice);
    }

    public void PlaySpecChallengeFail()
    {
        PlaySoundEffect(specChallengeFail);
    }

    public void PlaySpecChallengeSuccess()
    {
        PlaySoundEffect(specChallengeSuccess);
    }

    private void PlaySoundEffect(AudioClip audioClip)
    {

        if (!audioSource.isPlaying || audioSource.clip != audioClip)
        {
            audioSource.clip = audioClip;
            audioSource.Play();
        }
    }
}
