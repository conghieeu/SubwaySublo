using System.Collections;
using System.Collections.Generic;
using PauseManagement.Core;
using TMPro;
using UnityEngine;

public class UIReadyGo : MonoBehaviour
{
    [SerializeField] float countDownTime = 3f;
    [SerializeField] bool startCount;
    [SerializeField] TMP_Text txtTime;
    [SerializeField] RectTransform txtTouchToPlay;
    [SerializeField] RectTransform timer;

    bool blockCountDown;

    void OnEnable()
    {
        countDownTime = 3f;
    }

    void FixedUpdate()
    {
        OnCountDown();
    }

    private void OnCountDown()
    {
        if (countDownTime <= 0 && blockCountDown == false)
        {
            PauseManager.Instance.TogglePause();
            txtTime.text = "0";
            timer.gameObject.SetActive(true);
            print("start");
            gameObject.SetActive(false);
            blockCountDown = true;
            return;
        }
        if (startCount) countDownTime -= Time.fixedDeltaTime;
        txtTime.text = ((int)countDownTime).ToString();
    }

    public void OnTapToScreen()
    {
        txtTouchToPlay.gameObject.SetActive(false);
        txtTime.gameObject.SetActive(true);
        startCount = true;
    }
}
