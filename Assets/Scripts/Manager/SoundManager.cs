using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    [SerializeField] Slider volumeSlider;

    void Start()
    {
        volumeSlider.onValueChanged.AddListener((v) => { ChangeVolume(); });

        if (!PlayerPrefs.HasKey("musicVolume"))
        {
            PlayerPrefs.SetFloat("musicVolume", 1);
            Load();
        }
        else
        {
            Load();
        }

        AudioListener.volume = volumeSlider.value;

    }

    public void ChangeVolume()
    {
        AudioListener.volume = volumeSlider.value;
        save();
    }

    void Load()
    {
        volumeSlider.value = PlayerPrefs.GetFloat("musicVolume");
    }

    void save()
    {
        PlayerPrefs.SetFloat("musicVolume", volumeSlider.value);
    }
}
