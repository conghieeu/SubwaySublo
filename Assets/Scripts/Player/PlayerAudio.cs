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
    public AudioSource AudioSource { get => audioSource; set => audioSource = value; }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayAudio(AudioClip clip, bool isLoop)
    {
        // TRIGGER
        if (audioSource.clip == clip) return;

        AudioSource.loop = isLoop;
        AudioSource.clip = clip;
        AudioSource.Play();
    }

    public void PlayAudioRunClip()
    {
        AudioSource.clip = runClip;
        AudioSource.Play();
    }

    public void OnPauseGame()
    {
        AudioSource.Pause();
    }

    public void OnResumeGame()
    {
        AudioSource.Play();
    }
}
