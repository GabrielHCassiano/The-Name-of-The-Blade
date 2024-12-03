using System.Collections;
using System.Collections.Generic;
using Unity.Burst.Intrinsics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Windows;

public class PlayerSetStatus : MonoBehaviour
{
    private PlayerStatus playerStatus;
    // Start is called before the first frame update
    void Start()
    {
        playerStatus = GetComponentInParent<PlayerStatus>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UserPortion()
    {
        playerStatus.UserPortion();
    }

    public void ResetRecovery()
    {
        playerStatus.ResetRecovery();
    }

    public void ResetStatus()
    {
        playerStatus.ResetStatus();
    }

    public void StartCombo()
    {
        playerStatus.StartCombo();
    }

    public void ResetAttack()
    {
        playerStatus.ResetAttack();
    }

    public void SetForce(int value)
    {
        playerStatus.SetForce(value);
    }

    public void MoveAttack(float speed)
    {
        playerStatus.MoveAttack(speed);
    }

    public void ResetMoveAttack()
    {
        playerStatus.ResetMoveAttack();
    }

    public void StartParry()
    {
        playerStatus.StartParry();
    }

    public void EndParry()
    {
        playerStatus.EndParry();
    }

    public void EndDefese()
    {
        playerStatus.EndDefese();
    }

    public void EndStun()
    {
        playerStatus.EndStun();
    }

    public void GameOverMenu()
    {
        playerStatus.GameOverMenu();

    }
}
