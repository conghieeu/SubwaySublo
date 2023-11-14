using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SupportPlane : MonoBehaviour
{
    public float length, speed;
    public Transform otherPlane;

    void Start()
    {

    }


    void Update()
    {
        Move();
        RepeatPlane();
    }

    void Move()
    {
        this.transform.Translate(Vector3.back * speed, Space.World);
    }

    void RepeatPlane()
    {
        float temp = transform.position.z;

        if (temp < -200)
        {
            transform.position = new Vector3(0,0, otherPlane.position.z + 200);
            RandomModel();
        }
    }

    void RandomModel()
    {
        Debug.Log(("Reset children " + this.name, transform));

        
    }
}

