using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIReadyGo : MonoBehaviour
{
    [SerializeField] float countDownTime = 3;
    [SerializeField] bool startCount;
    [SerializeField] TMP_Text txtTime;
    [SerializeField] RectTransform txtTouchToPlay;
    [SerializeField] RectTransform timer;

    void OnEnable()
    {
        countDownTime = 3;
    }

    void FixedUpdate()
    {
        OnCountDown();
    }

    private void OnCountDown()
    {
        if (countDownTime <= 0)
        {
            // bắt đầu cho thằng nhân vật nó chạy
            GameManager.Instance.PlayerStartRun();
            txtTime.text = "0";

            timer.gameObject.SetActive(true);
            return;
        }

        if (startCount)
            countDownTime -= Time.fixedDeltaTime;

        txtTime.text = ((int)countDownTime).ToString();
    }

    public void OnTapToScreen()
    {
        txtTouchToPlay.gameObject.SetActive(false);
        txtTime.gameObject.SetActive(true);
        startCount = true;
    }
}
