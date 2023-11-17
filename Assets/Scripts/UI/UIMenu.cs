using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMenu : MonoBehaviour
{
    [SerializeField] UIReadyGo uIReadyGo;

    public void OnReadyToPlay()
    {
        GameManager.Instance.OnGameStart();
    }
}
