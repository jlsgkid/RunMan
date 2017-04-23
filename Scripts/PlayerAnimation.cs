﻿using UnityEngine;
using System.Collections;

public enum PlayerState {
    Idle,
    Running,
    MoveLeft,
    MoveRight,
    Jumping,
    Sliding,
    Death
}


public class PlayerAnimation : MonoBehaviour {
    public AudioSource footStep;
    //public Animation animation;
	private Animator anim;

    private PlayerState state = PlayerState.Idle;
    private PlayerMove playerMove;

    void Awake() {
        playerMove = this.GetComponent<PlayerMove>();
		anim = this.GetComponent<Animator> ();
    }

    // Update is called once per frame
    void Update() {
        if (GameController.gameState == GameState.Menu) {
            state = PlayerState.Idle;
        } else if (GameController.gameState == GameState.Playing) {
            state = PlayerState.Running;
            if (playerMove.nowLaneIndex != playerMove.targetLaneIndex) {
                if (playerMove.nowLaneIndex < playerMove.targetLaneIndex) {
                    state = PlayerState.MoveRight;
                } else {
                    state = PlayerState.MoveLeft;
                }
            } else if (playerMove.isJumping) {
                state = PlayerState.Jumping;
            } else if (playerMove.isSliding) {
                state = PlayerState.Sliding;
            }
        } else if (GameController.gameState == GameState.End) {
            state = PlayerState.Death;
        }
    }

    void LateUpdate() {
        switch (state) {
            case PlayerState.Idle:
                PlayIdle();
                break;
            case PlayerState.Running:
                PlayRunning();
                break;
            case PlayerState.MoveLeft:
                PlayLeft();
                break;
            case PlayerState.MoveRight:
                PlayRight();
                break;
            case PlayerState.Jumping:
                PlayJump();
                break;
            case PlayerState.Sliding:
                PlaySlide();
                break;
            case PlayerState.Death:
                PlayDeath();
                break;
        }
        if (state == PlayerState.Running || state == PlayerState.MoveRight || state == PlayerState.MoveLeft) {
            if (!footStep.isPlaying) {
                footStep.Play();
            }
        } else {
            footStep.Stop();
        }
    }

    void PlayRunning() {
//        if (!animation.IsPlaying("run")) {
//            animation.Play("run");
//        }
		anim.SetBool ("Run", true);
    }

    void PlayIdle() {
//        if (!animation.IsPlaying("Idle_1") && !animation.IsPlaying("Idle_2")) {
//            animation.Play("Idle_1");
//            animation.PlayQueued("Idle_2");
//        }
    }

    void PlayRight() {
//        if (!animation.IsPlaying("right")) {
//            animation.Play("right");
//        }
    }

    void PlayLeft() {
//        if (!animation.IsPlaying("left")) {
//            animation["left"].speed = 2;
//            animation.Play("left");
//        }
    }

    void PlayJump() {
//        if (!animation.IsPlaying("jump")) {
//            animation.Play("jump");
//        }
		anim.SetBool ("Jump", true);
    }
    void PlaySlide() {
//        if (!GetComponent<Animation>().IsPlaying("slide")) {
//            GetComponent<Animation>()["slide"].speed = 1.5f;
//            GetComponent<Animation>().Play("slide");
//        }
    }
    private bool havePlayDeath = false;
    void PlayDeath() {
//        if (havePlayDeath == false) {
//            havePlayDeath = true;
//            GetComponent<Animation>().Play("death");
//        }
    }

}
