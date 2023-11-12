using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.AI;

public class PlayerControllerMovement : MonoBehaviour
{
    float xRot;
    Vector2 PlayerMouseInput;
    Vector3 PlayerMovementInput;
    Vector3 velocity;
    int indexX;

    [SerializeField] private Transform playerCamera;
    [SerializeField] private CharacterController controller;
    [Space]
    [SerializeField] float speed;
    [SerializeField] float jumpForce;
    [SerializeField] float sensitivity;
    [SerializeField] float gravity;

    // Start is called before the first frame update
    void Start()
    {
        indexX = 1;
    }

    // Update is called once per frame
    void Update()
    {
        ControlPlayer();
        MoveX();

        // PlayerMovementInput = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
        //PlayerMouseInput = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

        // MovePlayerCamera();
    }

    void MoveX()
    {
        int[] a = new int[] {-2,0,2};
        int dir=0;

        // move x
        if(Input.GetKeyDown(KeyCode.A)) dir = -1;
        else if(Input.GetKeyDown(KeyCode.D)) dir = 1;

        float x = this.transform.position.x;

        if((indexX + dir) >= 0 && (indexX + dir) <= 2) {
            indexX = indexX + dir;
            x = a[indexX];
        }
        
        Vector3 pos = this.transform.position;
        pos.x = x;
        pos.z = 10;
        transform.position = Vector3.MoveTowards(this.transform.position, pos, 0.5f);
    }


    void ControlPlayer()
    {
        Vector3 moveVector = transform.TransformDirection(PlayerMovementInput);
        PlayerMovementInput = new Vector3(0f, 0f, 1);
        
        // y -= gravity * -2f * Time.deltaTime;
        if (controller.isGrounded)
        {
            velocity.y = -500 * Time.deltaTime;

            if (Input.GetKeyDown(KeyCode.Space))
            {
                velocity.y = jumpForce;
            }
        }
        else
        {
            velocity.y -= gravity * -2f * Time.deltaTime;
        }

        controller.Move(moveVector * speed * Time.deltaTime);
        controller.Move(velocity * Time.deltaTime);
    }

    void MovePlayerCamera()
    {
        xRot -= PlayerMouseInput.y;
        transform.Rotate(0f, PlayerMouseInput.x * sensitivity, 0f);
        playerCamera.transform.localRotation = Quaternion.Euler(xRot, 0f, 0f);
    }
}
