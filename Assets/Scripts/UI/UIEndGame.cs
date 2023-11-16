using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIEndGame : MonoBehaviour
{

    public void GoOnMenu()
    {
        Debug.Log("Go On Menu");
    }

    public void BtnRestart()
    {
        LevelManager.Instance.ReloadCurrentScene();
    }
}
