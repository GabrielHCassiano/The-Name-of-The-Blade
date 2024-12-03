using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    private EnemyStatus enemyStatus;
    private NavMeshAgent navMeshAgent;

    public EnemyMovement(EnemyStatus enemyStatus, NavMeshAgent navMeshAgent)
    {
        this.enemyStatus = enemyStatus;
        this.navMeshAgent = navMeshAgent;
    }

    public void MoveLogic()
    {
        if (enemyStatus.CanMove && enemyStatus.InTag)
        {
            navMeshAgent.SetDestination(enemyStatus.PlayerStatus.transform.position);
        }
        else
            navMeshAgent.velocity = Vector3.zero;
    }
}
