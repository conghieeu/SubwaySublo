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
    [SerializeField] ChuongNgaiVat obstacle;

    public MainPlane NextPlane { get => nextPlane; set => nextPlane = value; }

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
        if (transform.position.z > endPos) return;

        Debug.Log(("Reset vị trí cái plane này: " + this.name, transform));

        nextPlane = planeCtrl.getTheLastPlane();
        // Debug.Log(nextPlane.NextPlane, nextPlane.NextPlane.transform);

        transform.position = new Vector3(0, 0, nextPlane.transform.position.z + length - 0.5f);


        if (this.obstacle)
        {
            planeContainer.addObstacle(this.obstacle);
        }

        // lấy ngẫu nhiên modun mới từ plane container bỏ vào đây
        this.obstacle = planeContainer.getObstacle(isThisStartPlane);
        this.obstacle.transform.SetParent(this.transform);
        this.obstacle.parentPlane = this;
        this.obstacle.transform.localPosition = Vector3.zero;
        this.obstacle.gameObject.SetActive(true);

        isThisStartPlane = false;

        nextPlane.NextPlane = this;
        nextPlane = null;
    }

}

