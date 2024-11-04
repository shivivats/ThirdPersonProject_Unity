using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour {
    
    private Animator animator;
     [SerializeField] private PlayerController playerController;

    private string IS_WALKING = "isWalking";
    private string IS_RUNNING = "isRunning";

    private bool isWalking;
    private bool isRunning;
    
    private void Awake() {
        animator = GetComponent<Animator>();
    }

    private void Update() {
        HandleAnimation();
    }

    private void HandleAnimation() {
        isWalking = animator.GetBool(IS_WALKING);
        isRunning = animator.GetBool(IS_RUNNING);

        if (playerController.IsPlayerMoving() && !isWalking) {
            animator.SetBool(IS_WALKING, true);
        } else if (!playerController.IsPlayerMoving() && isWalking) {
            animator.SetBool(IS_WALKING, false);
        }

        if ((playerController.IsPlayerMoving() && playerController.IsPlayerRunning()) && !isRunning) {
            animator.SetBool(IS_RUNNING, true);
        } else if ((!playerController.IsPlayerMoving() || !playerController.IsPlayerRunning()) && isRunning) {
            animator.SetBool(IS_RUNNING, false);
        }
    }
}
