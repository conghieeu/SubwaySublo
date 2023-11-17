using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using SubwaySublo.UI;

public class UIMyRecord : MonoBehaviour
{
    [SerializeField] TMP_Text textRecord;
    [SerializeField] Timer timer;

    void OnEnable()
    {
        if (GameManager.Instance)
            textRecord.text = "Kỷ lục của bạn là " + timer.FloatToTime(
                SavePrefabs.Instance.LoadValue(SavePrefabs.SaveKeys.Highscore));
    }
}
