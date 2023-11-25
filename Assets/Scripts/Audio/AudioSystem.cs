using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SubwaySublo.Audio
{
    public class AudioSystem : MonoBehaviour
    {
        public AudioSource audioSource;
        
        [Space]
        public AudioClip clipEndGame;

        public static AudioSystem Instance { get; private set; }

        private void Awake()
        {
            if (Instance != null && Instance != this) Destroy(this);
            else Instance = this;

        }

        private void Start()
        {
            audioSource = GetComponent<AudioSource>();
        }

        public void PlayAudio(AudioClip clip)
        {
            if (audioSource.clip == clip) return;

            audioSource.clip = clip;
            audioSource.Play(); 
        }
    }
}

