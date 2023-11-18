using System.Collections;
using System.Collections.Generic;
using PauseManagement.Core;
using UnityEngine;

public class UIPauseGame : MonoBehaviour
{


    public void GoOnMenu()
    {
        GameManager.Instance.OnGoToMenu();
    }

    public void ContinueClick()
    {
        PauseManager.Instance.TogglePause();
    }

}
