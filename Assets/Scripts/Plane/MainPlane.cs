using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class MainPlane : MonoBehaviour
{
    [SerializeField] PlaneCtrl planeCtrl;
    [SerializeField] PlaneContainer planeContainer;

    [Space]
    [SerializeField] bool isThisStartPlane;
    [SerializeField] float endPos;
    [SerializeField] float length;
    [SerializeField] float speed;
    [SerializeField] float maxSpeed = 100f;
    [SerializeField] float speedAcceleration = 1f; // Tốc độ tăng lên
    [SerializeField] MainPlane nextPlane;
    [SerializeField] MainPlane backPlane;
    [SerializeField] ChuongNgaiVat obstacle;

    public MainPlane NextPlane { get => nextPlane; set => nextPlane = value; }
    public MainPlane BackPlane { get => backPlane; set => backPlane = value; }

    void Start()
    {
        planeCtrl = GetComponentInParent<PlaneCtrl>();
    }

    void FixedUpdate()
    {
        Move();
        ResetPlanePos();
        IncreaseSpeed();
    }

    public void LoadObstacle()
    {
        if (this.obstacle)
        {
            planeContainer.addObstacle(this.obstacle);
        }

        // lấy ngẫu nhiên modun mới từ plane container bỏ vào đây
        this.obstacle = planeContainer.getObstacle(isThisStartPlane);

        if (this.obstacle)
        {
            this.obstacle.transform.SetParent(this.transform);
            this.obstacle.parentPlane = this;
            this.obstacle.transform.localPosition = Vector3.zero;
            this.obstacle.gameObject.SetActive(true);
        }

        isThisStartPlane = false;
    }

    void Move()
    {
        if (planeCtrl.IsStopMovePlane == false)
        {
            this.transform.Translate(Vector3.back * speed, Space.World);
        }
    }

    void IncreaseSpeed()
    {
        if (speed < maxSpeed)
        {
            speed += speedAcceleration * Time.fixedDeltaTime;
        }
    }

    void ResetPlanePos()
    {
        if (transform.position.z > endPos) return;

        Debug.Log(("Reset vị trí cái plane này: " + this.name, transform));

        MainPlane lastPlane = planeCtrl.getTheLastPlane();



        transform.position = new Vector3(0, 0, lastPlane.transform.position.z + length - 0.5f);

        LoadObstacle();

        Debug.Log(lastPlane.name);
        nextPlane = lastPlane;
        lastPlane.backPlane = this;
        backPlane = null;
    }

}

