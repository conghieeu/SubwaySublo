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

    private void Awake()
    {
        if (Instance != null && Instance != this) Destroy(this);
        else Instance = this;


    }

    void Start()
    {
        UIEndGame.gameObject.SetActive(false);

        playerVirtualCamera.gameObject.SetActive(true);
    }

    void Update()
    {

        TogglePause();
    }

    void FixedUpdate()
    {
        CountTime();
    }

    public string TimeToString(float time)
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

    public float LoadElapsedTime()
    {
        // Lấy giá trị từ PlayerPrefs bằng khóa (key)
        float loadedValue = PlayerPrefs.GetFloat("Highscore");

        // Trả về giá trị đã lưu hoặc giá trị mặc định là 0 nếu không có dữ liệu
        return loadedValue;
    }

    public void EndGame()
    {
        if (isEndGame) return;
        isEndGame = true;

        Debug.Log("END GAME!!");

        txtTimer.gameObject.SetActive(false);
        UIEndGame.gameObject.SetActive(true);
        UIEndGame.GetComponent<Animator>().SetBool("IsEnd", true);

        if (elapsedTime > LoadElapsedTime())
        {
            SaveElapsedTime();
        }

        txtYourTime.text = "Your time: " + TimeToString(elapsedTime);
        txtBestRecord.text = "Best record: " + TimeToString(LoadElapsedTime());
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

    void SaveElapsedTime()
    {
        // Lưu giá trị vào PlayerPrefs với một khóa (key)
        PlayerPrefs.SetFloat("Highscore", elapsedTime);

        // Lưu thay đổi
        PlayerPrefs.Save();
    }

    void CountTime()
    {
        if (isEndGame) return;

        // Tăng thời gian tích lũy theo thời gian thực (Time.fixedDeltaTime)
        elapsedTime += Time.fixedDeltaTime;

        txtTimer.text = TimeToString(elapsedTime);
    }

}
