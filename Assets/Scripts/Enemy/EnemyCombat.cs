using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class EnemyCombat : MonoBehaviour
{
    private EnemyStatus enemyStatus;
    private Transform enemyTransform;
    private NavMeshAgent navMeshAgent;

    private float distance;
    private float attackCooldown = 0;

    public EnemyCombat(EnemyStatus enemyStatus, Transform transform, NavMeshAgent navMeshAgent)
    {
        this.enemyStatus = enemyStatus;
        this.enemyTransform = transform;
        this.navMeshAgent = navMeshAgent;
    }

    public void AttackLogic()
    {
        distance = Vector3.Distance(enemyStatus.PlayerStatus.transform.position, enemyTransform.position);

        if (enemyStatus.InAttack)
        {
            attackCooldown += 1 * Time.deltaTime;

            if (attackCooldown >= 200)
            {
                enemyStatus.InAttack = false;
                enemyStatus.CanAttack = true;
                attackCooldown = 0;



            }
        }

        if (enemyStatus.CanAttack && enemyStatus.InTag && distance <= 2)
        {
            enemyStatus.CanMove = false;
            enemyStatus.CanAttack = false;
            enemyStatus.InAttack = true;
            enemyStatus.Combo = 1;
            navMeshAgent.velocity = Vector3.zero;

            enemyStatus.Combo = enemyStatus.InCombo;

            if (enemyStatus.Combo < enemyStatus.ComboLenght)
                enemyStatus.Combo++;

            enemyStatus.InCombo = enemyStatus.Combo;
        }

    }
}
