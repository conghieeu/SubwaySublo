using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class PlayerControllerMovement : MonoBehaviour
{
    float xRot;
    Vector2 PlayerMouseInput;
    Vector3 PlayerMovementInput;
    Vector3 velocity;
    int indexX;

    [SerializeField] PlayerAnimator playerAnimator;
    [SerializeField] private Transform playerCamera;
    [SerializeField] private CharacterController controller;
    [Space]
    [SerializeField] float speed;
    [SerializeField] float jumpForce;
    [SerializeField] float sensitivity;
    [SerializeField] float gravity;
    [SerializeField] float fallSpeed;

    // Start is called before the first frame update
    void Start()
    {
        indexX = 1;
        fallSpeed = -500;
        playerAnimator = GetComponentInChildren<PlayerAnimator>();
    }

    // Update is called once per frame
    void Update()
    {
        ControlPlayer();
        MoveX();
        Roll();

        // PlayerMovementInput = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
        //PlayerMouseInput = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        // MovePlayerCamera();

    }

    void Roll()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            fallSpeed = -20;
            playerAnimator.RunRollingAnimation();
        }
    }

    void MoveX()
    {
        float moveSpeed = 50f;
        int[] a = new int[] { -2, 0, 2 };
        int dir = 0;

        // move x
        if (Input.GetKeyDown(KeyCode.A))
        {
            dir = -1;
            playerAnimator.RunTurnLeftAnimation();
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            dir = 1;
            playerAnimator.RunTurnRightAnimation();
        }

        float x = this.transform.position.x;

        if ((indexX + dir) >= 0 && (indexX + dir) <= 2)
        {
            indexX = indexX + dir;
            x = a[indexX];
        }

        Vector3 pos = this.transform.position;
        pos.x = x;
        pos.z = 10;
        transform.position = Vector3.MoveTowards(this.transform.position, pos, moveSpeed * Time.deltaTime);

        // Vật thể đã đến đích 
        if (Vector3.Distance(transform.position, pos) < 0.001f)
        {
            if (playerAnimator.PlayerState == PlayerAnimator.state.leftTurn ||
                playerAnimator.PlayerState == PlayerAnimator.state.rightTurn)
                playerAnimator.RunRunningAnimation();
        }
    }

    void ControlPlayer()
    {
        Vector3 moveVector = transform.TransformDirection(PlayerMovementInput);
        PlayerMovementInput = new Vector3(0f, 0f, 1);

        // y -= gravity * -2f * Time.deltaTime;
        if (controller.isGrounded)
        {
            velocity.y = -500 * Time.deltaTime;
            fallSpeed = -2;

            if (Input.GetKeyDown(KeyCode.Space))
            {
                velocity.y = jumpForce;
            }

            if (playerAnimator.PlayerState == PlayerAnimator.state.jumping)
            {
                playerAnimator.RunRunningAnimation();
            }
        }
        else
        {
            if (playerAnimator.PlayerState != PlayerAnimator.state.rolling)
            {
                playerAnimator.RunJumpAnimation();
            }

            velocity.y -= gravity * fallSpeed * Time.deltaTime;
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
