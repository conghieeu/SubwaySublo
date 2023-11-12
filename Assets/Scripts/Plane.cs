using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Plane : MonoBehaviour
{
    public float length, startpos;
    public Vector3 moveDirection;
    public Transform otherPlane;
    
    void Start(){
        startpos = transform.position.z;
    }


    void FixedUpdate()
    {
        Move();
        ParallaxEffect();
    }

    void Move(){
        this.transform.Translate(moveDirection, Space.World);
    }

    void ParallaxEffect() {
        float temp = transform.position.z;

        if (temp < -100) 
            transform.position = new Vector3(transform.position.x, transform.position.y, otherPlane.position.z + 200-1);
    }
}

