using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody rb;
    private PlayerInputs playerInputs;

    private PlayerStatus playerStatus;

    private GameObject camPoint;

    private Transform playerTransform;

    private Vector3 direction;
    private Vector2 mouseRotation;

    public PlayerMovement(Rigidbody rb, PlayerInputs playerInputs, PlayerStatus playerStatus, GameObject camPoint, Transform playerTransform)
    {
        this.rb = rb;
        this.playerInputs = playerInputs;
        this.playerStatus = playerStatus;
        this.camPoint = camPoint;
        this.playerTransform = playerTransform;
    }

    public void MoveLogic()
    {
        if (playerStatus.CanMove)
        {

            direction = new Vector3(playerInputs.InputDirection.x, 0, playerInputs.InputDirection.y);
            direction = direction.normalized;
            direction = camPoint.transform.TransformDirection(direction);
            direction.y = 0;

            playerStatus.AudioSource[3].Play();


            if (direction != Vector3.zero)
                playerStatus.AudioSource[3].mute = false;
            else
                playerStatus.AudioSource[3].mute = true;

            if (playerInputs.InputRun)
            {
                playerStatus.InRun = true;
                rb.velocity = direction * playerStatus.RunSpeed;
            }
            else
            {
                playerStatus.InRun = false;
                rb.velocity = direction * playerStatus.Speed;
            }
        }
    }

    public void DashLogic()
    {

    }

    public void JumpLogic()
    {

    }

    public void CamLogic()
    {
        if (playerStatus.CanCam)
        {
            if (direction != Vector3.zero)
                playerTransform.forward = Vector3.Slerp(playerTransform.forward, direction, Time.deltaTime * 7);
                //transform.eulerAngles = Vector3.up * Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;

            mouseRotation.x += playerInputs.MousePosition.x;
            mouseRotation.y += playerInputs.MousePosition.y / 2;

            mouseRotation.y = Mathf.Clamp(mouseRotation.y, -10, 70);

            camPoint.transform.rotation = Quaternion.Euler(mouseRotation.y, mouseRotation.x, 0);
        }
    }
}
