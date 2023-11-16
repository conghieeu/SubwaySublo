using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneCtrl : MonoBehaviour
{
    [SerializeField] bool isStopMovePlane;

    public static PlaneCtrl Instance { get; private set; }
    public bool IsStopMovePlane { get => isStopMovePlane; set => isStopMovePlane = value; }

    private void Awake()
    {
        if (Instance != null && Instance != this) Destroy(this);
        else Instance = this;

    }
    
}
