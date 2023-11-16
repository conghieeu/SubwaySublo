using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this) Destroy(this);
        else Instance = this;


    }
    // Load lại scene hiện tại
    public void ReloadCurrentScene()
    {
        // Lấy tên scene hiện tại
        string currentSceneName = SceneManager.GetActiveScene().name;

        // Hoặc lấy index của scene hiện tại
        // int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        // Load lại scene bằng cách sử dụng tên hoặc index
        SceneManager.LoadScene(currentSceneName);
        // Hoặc
        // SceneManager.LoadScene(currentSceneIndex);
    }

    // Chuyển đến scene mới
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    // Chuyển đến scene mới với hiệu ứng chuyển cảnh
    public void LoadSceneWithTransition(string sceneName, float transitionDuration)
    {
        // Implement logic chuyển cảnh với hiệu ứng (ví dụ: fade in/out)
        // sau đó gọi SceneManager.LoadScene(sceneName);
    }

    // Chuyển đến scene theo index
    public void LoadSceneByIndex(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }

    // Lấy tên của scene hiện tại
    public string GetCurrentSceneName()
    {
        return SceneManager.GetActiveScene().name;
    }

    // Lấy index của scene hiện tại
    public int GetCurrentSceneIndex()
    {
        return SceneManager.GetActiveScene().buildIndex;
    }

    // Thoát game
    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
