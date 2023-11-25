using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneCtrl : MonoBehaviour
{
    [SerializeField] bool isStopMovePlane;
    [SerializeField] List<MainPlane> mainPlanes;

    public static PlaneCtrl Instance { get; private set; }
    public bool IsStopMovePlane { get => isStopMovePlane; set => isStopMovePlane = value; }

    private void Awake()
    {
        if (Instance != null && Instance != this) Destroy(this);
        else Instance = this;

    }

    /// <summary> Lấy thằng Plane đang nằm cuối </summary>
    public MainPlane getTheLastPlane()
    {
        foreach (var p in mainPlanes)
        {
            if(p.BackPlane == null)  {
                return p;
            }
        }
        return null;
    }

}
