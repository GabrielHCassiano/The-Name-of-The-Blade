using System.Collections;
using System.Collections.Generic;
using Unity.Burst.Intrinsics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Windows;

public class EnemySetStatus : MonoBehaviour
{
    private EnemyStatus enemyStatus;

    private void Start()
    {
        enemyStatus = GetComponentInParent<EnemyStatus>();   
    }

    public void ResetStatus()
    {
        enemyStatus.ResetStatus();
    }

    public void StartCombo()
    {
        enemyStatus.StartCombo();
    }

    public void ResetAttack()
    {
        enemyStatus.ResetAttack();
    }

    public void SetForce(int value)
    {
        enemyStatus.SetForce(value);
    }

    public void MoveAttack(float speed)
    {
        enemyStatus.MoveAttack(speed);
    }

    public void ResetMoveAttack()
    {
        enemyStatus.ResetMoveAttack();
    }

    public void EndStun()
    {
        enemyStatus.EndStun();
    }
}
