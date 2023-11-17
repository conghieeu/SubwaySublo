using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIMyRecord : MonoBehaviour
{
    [SerializeField] TMP_Text textRecord;

    void OnEnable()
    {
        if (GameManager.Instance)
            textRecord.text = "Kỷ lục của bạn là " + GameManager.Instance.FloatToTime(
                SavePrefabs.Instance.LoadValue(SavePrefabs.SaveKeys.Highscore));
    }
}
