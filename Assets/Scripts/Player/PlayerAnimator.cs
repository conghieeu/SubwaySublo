using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    public enum state
    {
        running = 0,
        leftTurn = 1,
        rightTurn = 2,
        jumping = 3,
        rolling = 4,
        death = 5
    }

    private state playerState = state.running;
    private state playerStateTemp;

    PlayerCtrl playerController;
    Animator animator;

    public state PlayerState { get => playerState; set => playerState = value; }

    void Start()
    {
        playerController = GetComponentInParent<PlayerCtrl>();
        animator = GetComponent<Animator>();
    }

    public void RunDeathAnimation()
    {
        playerState = state.death;
        setAnimation();
    }

    public void RunRunningAnimation()
    {
        PlayerState = state.running;
        setAnimation();
    }

    public void RunRollingAnimation()
    {
        PlayerState = state.rolling;
        setAnimation();
    }


    public void RunTurnLeftAnimation()
    {
        PlayerState = state.leftTurn;
        setAnimation();
    }

    public void RunTurnRightAnimation()
    {
        PlayerState = state.rightTurn;
        setAnimation();
    }

    public void RunJumpAnimation()
    {
        PlayerState = state.jumping;
        setAnimation();
    }

    public void setAnimation()
    {
        if (playerState != playerStateTemp)
        {
            playerStateTemp = playerState;
            animator.SetInteger("state", (int)PlayerState);
        }
    }
}
