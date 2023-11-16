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
    [SerializeField] float speed;
    [SerializeField] float maxSpeed = 100f;
    [SerializeField] float speedAcceleration = 1f; // Tốc độ tăng lên
    [SerializeField] Transform otherPlane;
    [SerializeField] Transform model;
    [SerializeField] ChuongNgaiVat obstacle;

    public Transform Model { get => model; set => model = value; }

    void Start()
    {
        planeCtrl = GetComponentInParent<PlaneCtrl>();
    }

    void FixedUpdate()
    {
        Move();
        ParallaxEffect();
        IncreaseSpeed();
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
        // Tăng tốc độ dựa trên thời gian và tốc độ tăng lên
        speed += speedAcceleration * Time.fixedDeltaTime;

        // Giữ tốc độ trong khoảng từ 0 đến maxSpeed
        speed = Mathf.Clamp(speed, 0f, maxSpeed);

        // Áp dụng tốc độ vào nơi bạn muốn sử dụng nó trong game
        // Ví dụ: transform.Translate(Vector3.forward * currentSpeed * Time.fixedDeltaTime);

        // Kiểm tra xem tốc độ đã đạt đến giá trị tối đa hay chưa
        if (speed >= maxSpeed)
        {
            // Tốc độ đã đạt giá trị tối đa, có thể thực hiện các hành động cần thiết ở đây
        }
    }

    void ParallaxEffect()
    {
        float temp = transform.position.z;

        if (temp <= -200)
        {
            transform.position = new Vector3(0, 0, otherPlane.position.z + 200 - 1);
            OnResetChill();
        }
    }

    void OnResetChill()
    {
        Debug.Log(("Reset children " + this.name, transform));

        // Chuyển modun hiện tại qua plane container
        if (this.obstacle)
        {
            planeContainer.giveBackObstacle(this.obstacle, this);
        }

        // lấy ngẫu nhiên modun mới từ plane container bỏ vào đây
        this.obstacle = planeContainer.getObstacle();
        this.obstacle.transform.SetParent(this.Model);
        this.obstacle.parentPlane = this;
        this.obstacle.transform.localPosition = Vector3.zero;
        this.obstacle.gameObject.SetActive(true);

    }
}

