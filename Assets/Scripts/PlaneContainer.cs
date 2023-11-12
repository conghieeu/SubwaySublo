using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneContainer : MonoBehaviour
{
    [SerializeField] List<Transform> moduns = new List<Transform>();
    public Transform model;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public Transform getRandomModun()
    {
        int indexNgauNhien = Random.Range(0, moduns.Count);
        Transform modun = this.moduns[indexNgauNhien];

        return modun;
    }
}
