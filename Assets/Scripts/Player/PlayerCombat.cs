using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCombat : MonoBehaviour
{
    private Rigidbody rb;
    private PlayerInputs playerInputs;
    private PlayerStatus playerStatus;

    public PlayerCombat(Rigidbody rb, PlayerInputs playerInputs, PlayerStatus playerStatus)
    {
        this.rb = rb;
        this.playerInputs = playerInputs;
        this.playerStatus = playerStatus;
    }

    public void AttackLogic()
    {
        if (playerInputs.InputAttack && playerStatus.CanAttack)
        {
            playerStatus.CanMove = false;
            playerInputs.InputAttack = false;
            playerStatus.CanAttack = false;
            playerStatus.CanDefese = false;
            playerStatus.CanRecovery = false;
            playerStatus.InAttack = true;
            playerStatus.InRun = false;

            playerStatus.Combo = playerStatus.InCombo;

            if (playerStatus.Combo < 3)
                playerStatus.Combo++;

            playerStatus.InCombo = playerStatus.Combo;
        }
    }

    public void DefeseLogic()
    {
        if (playerInputs.InputDefese && playerStatus.CanDefese)
        {
            playerStatus.CanAttack = false;
            playerStatus.InDefese = true;
        }
        else if(!playerInputs.InputDefese && playerStatus.CanDefese && playerStatus.InDefese)
        {
            playerStatus.CanAttack = true;
            playerStatus.InDefese = false;
        }
    }

    public void RecoveryLogic()
    {
        if (playerInputs.InputRecover && playerStatus.CanRecovery)
        {
            playerInputs.InputRecover = false;
            playerStatus.CanRecovery = false;
            playerStatus.CanAttack = false;
            playerStatus.CanDefese = false;
            playerStatus.InRecovery = true;
        }
    }

}
