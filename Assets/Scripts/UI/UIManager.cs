using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] List<Transform> vatTheMuonTatKhiKhoiDong;

    void Start()
    {
        foreach (var obj in vatTheMuonTatKhiKhoiDong)
        {
            obj.gameObject.SetActive(false);
        }
    }
}
