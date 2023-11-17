using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePrefabs : MonoBehaviour
{
    public enum SaveKeys
    {
        Highscore = 0,
    }

    public static SavePrefabs Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this) Destroy(this);
        else Instance = this;

    }



    public float LoadValue(SaveKeys key)
    {
        float loadedValue = PlayerPrefs.GetFloat(key.ToString());
        return loadedValue;
    }

    public void SaveValue(SaveKeys key, float value)
    {
        PlayerPrefs.SetFloat(key.ToString(), value);
        PlayerPrefs.Save();
    }
}


