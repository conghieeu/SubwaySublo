using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class MainPlane : MonoBehaviour
{
    public PlaneContainer planeContainer;
    public float length, speed;
    [Space]
    public Transform otherPlane;
    public Transform model;
    public ChuongNgaiVat obstacle;

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
            transform.position = new Vector3(0,0, otherPlane.position.z + 200 - 1);
            OnResetChill();
        }
    }

    void OnResetChill()
    {
        Debug.Log(("Reset children " + this.name, transform));

        // Chuyển modun hiện tại qua plane container
        if(this.obstacle)
        {
            planeContainer.giveBackObstacle(this.obstacle, this);
        }

        // lấy ngẫu nhiên modun mới từ plane container bỏ vào đây
        this.obstacle = planeContainer.getObstacle();
        this.obstacle.transform.SetParent(this.model);
        this.obstacle.parentPlane = this;
        this.obstacle.transform.localPosition = Vector3.zero;
        this.obstacle.gameObject.SetActive(true);

    }
}

