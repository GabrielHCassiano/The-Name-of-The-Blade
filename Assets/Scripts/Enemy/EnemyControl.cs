using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyControl : MonoBehaviour
{
    private Rigidbody rb;
    private NavMeshAgent navMeshAgent;
    private Animator animator;

    private EnemyStatus enemyStatus;
    private EnemyMovement enemyMovement;
    private EnemyCombat enemyCombat;
    private EnemyAnimation enemyAnimation;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();

        enemyStatus = GetComponent<EnemyStatus>();

        enemyStatus.ResetStatus();
        enemyMovement = new EnemyMovement(enemyStatus, navMeshAgent);
        enemyCombat = new EnemyCombat(enemyStatus, transform, navMeshAgent);
        enemyAnimation = new EnemyAnimation(animator, enemyStatus, navMeshAgent);
    }

    // Update is called once per frame
    void Update()
    {
        enemyStatus.RayCast();
        enemyStatus.PostureLogic();
        enemyCombat.AttackLogic();
        enemyAnimation.AnimationLogic();
        enemyStatus.BarLogic();
        enemyStatus.Dead();
    }

    private void FixedUpdate()
    {
        enemyMovement.MoveLogic();
    }
}
