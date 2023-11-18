using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : MonoBehaviour
{
    float xRot;
    Vector2 PlayerMouseInput;
    Vector3 PlayerMovementInput;
    Vector3 velocity;
    int indexX;
    PlayerAnimator playerAnimator;
    Transform playerCamera;
    CharacterController charCtrl;
    [SerializeField] PlayerDeathColl playerDeath;

    [Space]
    [SerializeField] bool isPauseGame;
    [SerializeField] bool isDeath;
    [SerializeField] float speed;
    [SerializeField] float jumpForce = 15;
    [SerializeField] float cameraSensitivity;
    [SerializeField] float gravity = -20;
    [SerializeField] float fallSpeed = 20;
    [SerializeField] float moveXSpeed = 90;

    public static PlayerCtrl Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this) Destroy(this);
        else Instance = this;

    }

    void Start()
    {
        indexX = 1;
        fallSpeed = -500;
        playerAnimator = GetComponentInChildren<PlayerAnimator>();
        playerDeath = GetComponentInChildren<PlayerDeathColl>();
        charCtrl = GetComponent<CharacterController>();
    }

    void Update()
    {
        if (isDeath == false)
        {
            if (isPauseGame == false)
            {
                ControlPlayer();
                MoveX();
                Roll();
            }
        }
        else // PLAYER DEATH
        {
            Death();
        }
    }

    public void SetPauseGame(bool value)
    {
        isPauseGame = value;
    }

    private void Death()
    {
        velocity.y -= gravity * fallSpeed * Time.deltaTime;
        charCtrl.Move(velocity * Time.deltaTime);

        GameManager.Instance.EndGame();
    }

    public void SetDeathState()
    {
        PlaneCtrl.Instance.IsStopMovePlane = true;
        isDeath = true;
        playerAnimator.RunDeathAnimation();
    }

    void Roll()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            fallSpeed = -20;
            playerAnimator.RunRollingAnimation();

            // Setting charCtrl
            charCtrl.center = new Vector3(0, -0.5f, 0);
            charCtrl.height = 0.5f;
            // setting boxCast_death
            playerDeath.transform.localPosition = new Vector3(0, -0.5f, 0);
            playerDeath.BoxSize = new Vector3(0.3f, 0.25f, 0.3f);

        }
    }

    public void EndRoll()
    {
        // Setting charCtrl
        charCtrl.center = new Vector3(0, 0, 0);
        charCtrl.height = 1; 
        // setting boxCast_death
        playerDeath.transform.localPosition = new Vector3(0, 0f, 0);
        playerDeath.BoxSize = new Vector3(0.3f, 0.5f, 0.3f);
    }

    void MoveX()
    {
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
        transform.position = Vector3.MoveTowards(this.transform.position, pos, moveXSpeed * Time.deltaTime);

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
        if (charCtrl.isGrounded)
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

        charCtrl.Move(moveVector * speed * Time.deltaTime);
        charCtrl.Move(velocity * Time.deltaTime);
    }

    void MovePlayerCamera()
    {
        xRot -= PlayerMouseInput.y;
        transform.Rotate(0f, PlayerMouseInput.x * cameraSensitivity, 0f);
        playerCamera.transform.localRotation = Quaternion.Euler(xRot, 0f, 0f);
    }
}
