using System.Collections;
using System.Collections.Generic;
using PauseManagement.Core;
using UnityEngine;

public class PlayerAudio : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;
    [Space]
    [SerializeField] AudioClip deathClip;
    [SerializeField] AudioClip fallClip;
    [SerializeField] AudioClip jumpClip;
    [SerializeField] AudioClip runClip;
    [SerializeField] AudioClip moveClip;

    bool isDisable;

    public AudioClip DeathClip { get => deathClip; set => deathClip = value; }
    public AudioClip FallClip { get => fallClip; set => fallClip = value; }
    public AudioClip JumpClip { get => jumpClip; set => jumpClip = value; }
    public AudioClip RunClip { get => runClip; set => runClip = value; }
    public AudioClip MoveClip { get => moveClip; set => moveClip = value; }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    

    public void PlayClip(AudioClip clip)
    {
        audioSource.clip = clip;
        audioSource.Play();
    }

    public void PlayAudio(AudioClip clip, bool isLoop)
    {
        // TRIGGER
        if (audioSource.clip == clip) return;

        audioSource.loop = isLoop;
        audioSource.clip = clip;
        audioSource.Play();
    }

    public void PlayAudioRunClip()
    {
        audioSource.clip = runClip;
        audioSource.Play();
    }

    public void PauseHandle(bool paused)
    {
        if (paused) GetComponent<AudioSource>().Pause();
        else GetComponent<AudioSource>().Play();
    }


}
