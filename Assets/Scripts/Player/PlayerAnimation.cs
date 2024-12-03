using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAnimation : MonoBehaviour
{
    private Animator animator;
    private PlayerInputs playerInputs;
    private PlayerStatus playerStatus;

    public PlayerAnimation(Animator animator, PlayerInputs playerInputs, PlayerStatus playerStatus)
    {
        this.animator = animator;
        this.playerInputs = playerInputs;
        this.playerStatus = playerStatus;
    }

    public void AnimLogic()
    {
        animator.SetFloat("Horizontal", playerInputs.InputDirection.x);
        animator.SetFloat("Vertical", playerInputs.InputDirection.y);
        animator.SetBool("Hurt", playerStatus.InHurt);
        animator.SetBool("Stun", playerStatus.InStun);
        animator.SetBool("Dead", playerStatus.InDead);
        animator.SetBool("Run", playerStatus.InRun);
        animator.SetTrigger("Attack"+playerStatus.Combo);
        animator.SetBool("Defese", playerStatus.InDefese);
        animator.SetBool("Recovery", playerStatus.InRecovery);
    }
}
