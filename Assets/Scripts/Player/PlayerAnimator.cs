using System.Collections;
using System.Collections.Generic;
using PauseManagement.Core;
using UnityEngine;

namespace SubwaySublo.Player
{
    public enum State
    {
        Run = 0,
        TurnLeft = 1,
        TurnRight = 2,
        Jump = 3,
        Roll = 4,
        Death = 5
    }

    public class PlayerAnimator : MonoBehaviour
    {
        [SerializeField] State animState;

        PlayerCtrl playerCtrl;
        Animator anim;

        public State AnimState { get => animState; set => animState = value; }

        void Start()
        {
            playerCtrl = GetComponentInParent<PlayerCtrl>();
            anim = GetComponent<Animator>();
        }



        public void PlayClip(AudioClip clip) => playerCtrl.playerAudio.PlayClip(clip);

        public bool IsAnimationStop(State state)
        {
            if (animState != state) return true;
            if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f && animState == state) return true;
            return false;
        }

        public void SetAnimation(State state)
        {
            if (animState == state) return;
            animState = state;

            print("PLAYER ANIMATION: " + state.ToString());
            anim.Play("Base Layer." + state.ToString(), 0, 0f);
        }

        public void PauseHandle(bool paused)
        {
            if (paused) anim.Play("Base Layer.PauseGame", 0, 0f);
            else SetAnimation(State.Run);
        }
    }
}
