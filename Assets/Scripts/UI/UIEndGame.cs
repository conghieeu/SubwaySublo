using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using SubwaySublo.UI;

public class UIEndGame : MonoBehaviour
{
    [SerializeField] TMP_Text txtTimer;
    [SerializeField] TMP_Text txtYourTime;
    [SerializeField] TMP_Text txtBestRecord;
    [SerializeField] Timer timer;

    public void GoOnMenu()
    {
        GameManager.Instance.OnGoToMenu();
    }

    public void BtnRestart()
    {
        GameManager.Instance.OnGoToMenu();
    }

    public void OnEndGame()
    {
        gameObject.SetActive(true);
        GetComponent<Animator>().SetBool("IsEnd", true);
        SetYourTime(timer.txtTimer.text);
        SetBestRecord(timer.FloatToTime(SavePrefabs.Instance.LoadValue(SavePrefabs.SaveKeys.Highscore)));
    }

    public void SetYourTime(string value)
    {
        txtYourTime.text = "Thời gian: " + value;
    }

    public void SetBestRecord(string value)
    {
        txtBestRecord.text = "Kỷ lục là: " + value;
    }
}
