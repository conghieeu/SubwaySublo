using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace SubwaySublo.UI
{
    public class Timer : MonoBehaviour
    {
        [Header("Đếm thời gian")]
        public TMP_Text txtTimer;
        [SerializeField] float elapsedTime = 0f; // Thời gian đã trôi qua tích lũy

        void FixedUpdate()
        {
            CountTime();
        }

        public void OnEndGame()
        {
            gameObject.SetActive(false);
            SaveBestRecord();
        }

        public string FloatToTime(float time)
        {
            // Chuyển đổi giây thành phút và giây
            int minutes = Mathf.FloorToInt(time / 60);
            int seconds = Mathf.FloorToInt(time % 60);

            // Format thời gian thành chuỗi "00:00"
            string timerString = string.Format("{0:00}:{1:00}", minutes, seconds);

            // Gán giá trị chuỗi đã định dạng vào TextMeshPro trên UI
            return timerString;
        }

        void CountTime()
        {
            if (!GameManager.Instance.IsEndGame)
            {
                elapsedTime += Time.fixedDeltaTime;
                txtTimer.text = FloatToTime(elapsedTime);
            }
        }

        void SaveBestRecord()
        {
            float elapsedTimeSaved = SavePrefabs.Instance.LoadValue(SavePrefabs.SaveKeys.Highscore);

            if (elapsedTime > elapsedTimeSaved)
            {
                SavePrefabs.Instance.SaveValue(SavePrefabs.SaveKeys.Highscore, elapsedTime);
            }
        }
    }

}