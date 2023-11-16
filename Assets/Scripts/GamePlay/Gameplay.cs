using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Gameplay : MonoBehaviour
{
    [SerializeField] TMP_Text timerText;
    [SerializeField] UIEndGame uiEndGame;

    [Header("Đếm thời gian")]
    [SerializeField] float elapsedTime = 0f; // Thời gian đã trôi qua tích lũy
    [SerializeField] float totalTime = 120;  // Thời gian tối đa

    [Space]
    [SerializeField] bool isEndGame = false;

    public static Gameplay Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this) Destroy(this);
        else Instance = this;


    }

    void Start()
    {
        uiEndGame.gameObject.SetActive(false);
    }

    void FixedUpdate()
    {
        CountTime();
    }

    public void SetTextTime(float time, TMP_Text TMPText)
    {
        // Kiểm tra nếu thời gian tích lũy vượt quá thời gian tối đa, giữ nó ở thời gian tối đa
        elapsedTime = Mathf.Min(elapsedTime, totalTime);

        // Chuyển đổi giây thành phút và giây
        int minutes = Mathf.FloorToInt(elapsedTime / 60);
        int seconds = Mathf.FloorToInt(elapsedTime % 60);

        // Format thời gian thành chuỗi "00:00"
        string timerString = string.Format("{0:00}:{1:00}", minutes, seconds);

        // Gán giá trị chuỗi đã định dạng vào TextMeshPro trên UI
        timerText.text = timerString;
    }

    public float LoadElapsedTime()
    {
        // Lấy giá trị từ PlayerPrefs bằng khóa (key)
        float loadedValue = PlayerPrefs.GetFloat("elapsedTime", elapsedTime);

        // Trả về giá trị đã lưu hoặc giá trị mặc định là 0 nếu không có dữ liệu
        return loadedValue;
    }

    public void EndGame()
    {
        if (isEndGame) return;
        isEndGame = true;

        Debug.Log("END GAME!!");

        timerText.gameObject.SetActive(false);
        uiEndGame.gameObject.SetActive(true);
        uiEndGame.GetComponent<Animator>().SetBool("IsEnd", true);

        if (elapsedTime > LoadElapsedTime())
        {
            SaveElapsedTime();
        }

        SetTextTime(LoadElapsedTime(), uiEndGame. );
    }

    void SaveElapsedTime()
    {
        // Lưu giá trị vào PlayerPrefs với một khóa (key)
        PlayerPrefs.SetFloat("elapsedTime", elapsedTime);

        // Lưu thay đổi
        PlayerPrefs.Save();
    }

    void CountTime()
    {
        if (isEndGame) return;

        // Tăng thời gian tích lũy theo thời gian thực (Time.fixedDeltaTime)
        elapsedTime += Time.fixedDeltaTime;

        SetTextTime(elapsedTime, timerText);
    }

}
