using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MainPlane : MonoBehaviour
{
    public PlaneContainer planeContainer;
    public float length, speed;
    [Space]
    public Transform otherPlane;
    public Transform model;
    public Transform currentModun;

    void Start()
    { 

    }


    void FixedUpdate()
    {
        Move();
        ParallaxEffect();
    }

    void Move()
    {
        this.transform.Translate(Vector3.back * speed, Space.World);
    }

    void ParallaxEffect()
    {
        float temp = transform.position.z;

        if (temp <= -200)
        {
            transform.position = new Vector3(0,0, otherPlane.position.z + 200);
            OnResetChill();
        }
    }

    void OnResetChill()
    {
        Debug.Log(("Reset children " + this.name, transform));

        // Chuyển modun hiện tại qua plane container
        if(this.currentModun)
        {
            this.currentModun.SetParent(planeContainer.model);
            this.currentModun.localPosition = Vector3.zero;
            this.currentModun.gameObject.SetActive(false);
        }

        // lấy ngẫu nhiên modun mới từ plane container bỏ vào đây
        this.currentModun = planeContainer.getRandomModun();
        this.currentModun.SetParent(this.model);
        this.currentModun.localPosition = Vector3.zero;
        this.currentModun.gameObject.SetActive(true);

    }
}

