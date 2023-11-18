using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] UIPauseGame uIPauseGame;
    [SerializeField] UIMenu uIMenu;
    [SerializeField] UIEndGame uIEndGame;
    [Space]
    [SerializeField] List<Transform> vatTheMuonTatKhiKhoiDong;

    void Start()
    {

        foreach (var obj in vatTheMuonTatKhiKhoiDong)
        {
            obj.gameObject.SetActive(false);
        }

        uIMenu.gameObject.SetActive(true);
    }

    public void OnPauseEvent()
    {
        // Set Active UIPauseGame
        if (uIMenu.gameObject.activeSelf == false && uIEndGame.gameObject.activeSelf == false)
        {
            uIPauseGame.gameObject.SetActive(true);
        }

        
    }

    public void OnResumeEvent()
    {
        // Set 
        uIPauseGame.gameObject.SetActive(false);
    }
}
