using System.Collections;
using System.Collections.Generic;
using PauseManagement.Core;
using Unity.VisualScripting;
using UnityEngine;

namespace SubwaySublo.Player
{
    public class PlayerCtrl : MonoBehaviour
    {
        public PlayerAudio playerAudio;
        public PlayerDeathColl playerDeath;
        public PlayerAnimator playerAnimator;

        [Space]
        [SerializeField] float speed;
        [SerializeField] float jumpForce = 15;
        [SerializeField] float gravity = -20;
        [SerializeField] float fallSpeed = 20;
        [SerializeField] float moveXSpeed = 90;

        [Header("State")]
        [SerializeField] bool isRolling;
        [SerializeField] bool isTurning;
        [SerializeField] bool isJumping;

        Vector3 PlayerMovementInput;
        Vector3 velocity;
        int indexX;
        CharacterController characterController;

        public static PlayerCtrl Instance { get; private set; }

        private void Awake()
        {
            if (Instance != null && Instance != this) Destroy(this);
            else Instance = this;
        }

        void Start()
        {
            indexX = 1;
            playerAnimator = GetComponentInChildren<PlayerAnimator>();
            playerDeath = GetComponentInChildren<PlayerDeathColl>();
            characterController = GetComponent<CharacterController>();
            playerAudio = GetComponentInChildren<PlayerAudio>();

            PauseManager.PauseAction += PauseHandler;
        }

        void OnDestroy()
        {
            PauseManager.PauseAction -= PauseHandler;
        }

        void Update()
        {
            Behavior();
        }

        void PauseHandler(bool pause)
        {
            playerAudio.PauseHandle(pause);
            playerAnimator.PauseHandle(pause);
        }

        void Behavior()
        {
            if (!playerDeath.IsDeath)
            {
                if (!PauseManager.IsPaused)
                {
                    Jumping();
                    Turning();
                    Rolling();
                    Running();
                }
            }
            else
            {
                Death();
            }
        }

        void Running()
        {
            if (isTurning || isRolling || isJumping) return;

            SetAnimation(State.Run);
        }

        void Death()
        {
            print("PLAYER DEATH");

            // SET VELOCITY
            velocity.y -= gravity * fallSpeed * Time.deltaTime;
            characterController.Move(velocity * Time.deltaTime);

            SetAnimation(State.Death);
            PlayAudio(playerAudio.DeathClip, false);

            PlaneCtrl.Instance.IsStopMovePlane = true;
            GameManager.Instance.EndGame();
        }

        void Rolling()
        {
            // IN
            if (Input.GetKeyDown(KeyCode.S))
            {
                fallSpeed = -20;

                DisableAllState(); isRolling = true;
                SetAnimation(State.Roll);
                PlayAudio(playerAudio.FallClip, false);

                // CHANGE SIZE COLLIDER
                characterController.center = new Vector3(0, -0.5f, 0);
                characterController.height = 0.5f;
                // CHANGE SIZE BOXCAST
                playerDeath.transform.localPosition = new Vector3(0, -0.5f, 0);
                playerDeath.BoxSize = new Vector3(0.3f, 0.25f, 0.3f);
            }
            // OUT
            else if (playerAnimator.IsAnimationStop(State.Roll))
            {
                isRolling = false;

                // RESET SIZE COLLIDER
                characterController.center = new Vector3(0, 0, 0);
                characterController.height = 1;
                // RESET SIZE BOXCAST
                playerDeath.transform.localPosition = new Vector3(0, 0f, 0);
                playerDeath.BoxSize = new Vector3(0.3f, 0.5f, 0.3f);
            }
        }

        // MOVE LEFT, RIGHT
        void Turning()
        {
            int[] a = new int[] { -2, 0, 2 };
            int dir = 0;
            float x = this.transform.position.x;

            // INPUT
            if (Input.GetKeyDown(KeyCode.A))
            {
                dir = -1;
                isTurning = true;
                PlayAudio(playerAudio.MoveClip, false);
                SetAnimation(State.TurnLeft);
            }
            else if (Input.GetKeyDown(KeyCode.D))
            {
                dir = 1;
                isTurning = true;
                PlayAudio(playerAudio.MoveClip, false);
                SetAnimation(State.TurnRight);
            }

            if ((indexX + dir) >= 0 && (indexX + dir) <= 2)
            {
                indexX = indexX + dir;
                x = a[indexX];
            }

            Vector3 pos = this.transform.position;
            pos.x = x;
            pos.z = 10;
            transform.position = Vector3.MoveTowards(this.transform.position, pos, moveXSpeed * Time.deltaTime);

            // ĐÃ DI CHUYỄN XONG
            if (Vector3.Distance(transform.position, pos) < 0.001f)
            {
                isTurning = false;
            }
        }

        void Jumping()
        {
            Vector3 moveVector = transform.TransformDirection(PlayerMovementInput);
            PlayerMovementInput = new Vector3(0f, 0f, 1);

            if (characterController.isGrounded)
            {
                // SET VELOCITY
                velocity.y = -500 * Time.deltaTime;
                fallSpeed = -2;

                if (Input.GetKeyDown(KeyCode.Space))
                {
                    velocity.y = jumpForce;
                    PlayAudio(playerAudio.JumpClip, false);
                }

                isJumping = false;
            }
            else
            {
                if (!isRolling)
                {
                    isJumping = true;
                    SetAnimation(State.Jump);
                }
                velocity.y -= gravity * fallSpeed * Time.deltaTime;
            }

            characterController.Move(moveVector * speed * Time.deltaTime);
            characterController.Move(velocity * Time.deltaTime);
        }

        void DisableAllState() { isJumping = false; isTurning = false; isRolling = false; }
        void SetAnimation(State state) => playerAnimator.SetAnimation(state);
        void PlayAudio(AudioClip clip, bool isLoop) => playerAudio.PlayAudio(clip, isLoop);

    }

}