using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Cinemachine;

public class GameManager : MonoBehaviour
{
    public bool isPaused = false;

    [SerializeField] TMP_Text txtTimer;
    [SerializeField] TMP_Text txtYourTime;
    [SerializeField] TMP_Text txtBestRecord;
    [SerializeField] UIEndGame UIEndGame;

    [Space]
    [SerializeField] CinemachineVirtualCamera playerVirtualCamera;
    [SerializeField] CinemachineVirtualCamera menuVirtualCamera;

    [Header("Đếm thời gian")]
    [SerializeField] float elapsedTime = 0f; // Thời gian đã trôi qua tích lũy
    [SerializeField] float totalTime = 120;  // Thời gian tối đa

    [Space]
    [SerializeField] bool isEndGame = false;

    public static GameManager Instance { get; private set; }
    public bool IsEndGame { get => isEndGame; set => isEndGame = value; }

    private void Awake()
    {
        if (Instance != null && Instance != this) Destroy(this);
        else Instance = this;


    }

    void Start()
    {
        isPaused = true;
        TogglePause();
    }

    void Update()
    {

        TogglePause();
    }

    void FixedUpdate()
    {
        CountTime();
    }

    public void OnGameStart()
    {
        playerVirtualCamera.gameObject.SetActive(true);
        menuVirtualCamera.gameObject.SetActive(false);
    }

    public void OnGoToMenu()
    {
        playerVirtualCamera.gameObject.SetActive(false);
        menuVirtualCamera.gameObject.SetActive(true);
    }

    public void PlayerStartRun()
    {
        Time.timeScale = 1f;
    }

    public string FloatToTime(float time)
    {
        // Kiểm tra nếu thời gian tích lũy vượt quá thời gian tối đa, giữ nó ở thời gian tối đa
        time = Mathf.Min(time, totalTime);

        // Chuyển đổi giây thành phút và giây
        int minutes = Mathf.FloorToInt(time / 60);
        int seconds = Mathf.FloorToInt(time % 60);

        // Format thời gian thành chuỗi "00:00"
        string timerString = string.Format("{0:00}:{1:00}", minutes, seconds);

        // Gán giá trị chuỗi đã định dạng vào TextMeshPro trên UI
        return timerString;
    }

    public void EndGame()
    {
        if (IsEndGame) return;
        IsEndGame = true;
        float elapsedTimeSaved = SavePrefabs.Instance.LoadValue(SavePrefabs.SaveKeys.Highscore);

        Debug.Log("END GAME!!");

        txtTimer.gameObject.SetActive(false);
        UIEndGame.gameObject.SetActive(true);
        UIEndGame.GetComponent<Animator>().SetBool("IsEnd", true);

        if (elapsedTime > elapsedTimeSaved)
        {
            SavePrefabs.Instance.SaveValue(SavePrefabs.SaveKeys.Highscore, elapsedTime);
            elapsedTimeSaved = elapsedTime;
        }

        txtYourTime.text = "Your time: " + FloatToTime(elapsedTime);
        txtBestRecord.text = "Best record: " + FloatToTime(elapsedTimeSaved);
    }

    void TogglePause()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isPaused = !isPaused;
        }

        if (isPaused)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }
    }


    void CountTime()
    {
        if (IsEndGame) return;

        // Tăng thời gian tích lũy theo thời gian thực (Time.fixedDeltaTime)
        elapsedTime += Time.fixedDeltaTime;

        txtTimer.text = FloatToTime(elapsedTime);
    }

}
