using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Cinemachine;
using PauseManagement.Core;
using SubwaySublo.UI;
using SubwaySublo.Audio;

public class GameManager : MonoBehaviour
{
    [SerializeField] UIEndGame endGame;
    [SerializeField] Timer timer;

    [Space]
    [SerializeField] CinemachineVirtualCamera playerVirtualCamera;
    [SerializeField] CinemachineVirtualCamera menuVirtualCamera;

    [Space]
    [SerializeField] bool isEndGame = false;

    public bool IsEndGame { get => isEndGame; set => isEndGame = value; }
    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this) Destroy(this);
        else Instance = this;
    }

    void Start()
    {
        PauseManager.Instance.Pause();
    }


    public void OnGameStart()
    {
        playerVirtualCamera.gameObject.SetActive(true);
        menuVirtualCamera.gameObject.SetActive(false);
    }

    public void OnGoToMenu()
    {
        // playerVirtualCamera.gameObject.SetActive(false);
        // menuVirtualCamera.gameObject.SetActive(true);
        LevelManager.Instance.ReloadCurrentScene();
    }


    public void EndGame()
    {
        if (IsEndGame) return;
        IsEndGame = true;

        Debug.Log("END GAME!!");
        AudioSystem.Instance.PlayAudio(AudioSystem.Instance.clipEndGame);

        timer.OnEndGame();
        endGame.OnEndGame();

    }
}
