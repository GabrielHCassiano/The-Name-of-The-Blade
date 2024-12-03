using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class EnemyAnimation : MonoBehaviour
{
    private Animator animator;
    private EnemyStatus enemyStatus;
    private NavMeshAgent navMeshAgent;

    public EnemyAnimation(Animator animator, EnemyStatus enemyStatus, NavMeshAgent navMeshAgent)
    {
        this.animator = animator;
        this.enemyStatus = enemyStatus;
        this.navMeshAgent = navMeshAgent;
    }

    public void AnimationLogic()
    {
        animator.SetFloat("Horizontal", navMeshAgent.velocity.x);
        animator.SetFloat("Vertical", navMeshAgent.velocity.y);
        animator.SetBool("Hurt", enemyStatus.InHurt);
        animator.SetBool("Stun", enemyStatus.InStun);
        animator.SetBool("Dead", enemyStatus.InDead);
        animator.SetTrigger("Attack" + enemyStatus.Combo);
    }
}
