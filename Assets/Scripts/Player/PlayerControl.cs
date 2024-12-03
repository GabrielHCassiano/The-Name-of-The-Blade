using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    private Rigidbody rb;
    private Animator animator;
    private PlayerInputs playerInputs;

    private PlayerStatus playerStatus;
    private PlayerMovement playerMovement;
    private PlayerCombat playerCombat;
    private PlayerAnimation playerAnimation;


    [SerializeField] private GameObject camPoint;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponentInChildren<Animator>();
        playerInputs = GetComponentInChildren<PlayerInputs>();

        playerStatus = GetComponent<PlayerStatus>();
        playerStatus.ResetStatus();

        playerMovement = new PlayerMovement(rb, playerInputs, playerStatus, camPoint, gameObject.transform);
        playerCombat = new PlayerCombat(rb, playerInputs, playerStatus);
        playerAnimation = new PlayerAnimation(animator, playerInputs, playerStatus);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        playerStatus.PostureLogic();
        playerCombat.AttackLogic();
        playerCombat.DefeseLogic();
        playerCombat.RecoveryLogic();
        playerMovement.DashLogic();
        playerAnimation.AnimLogic();
        playerStatus.LevelLogic();
        playerStatus.BarLogic();
        playerStatus.Dead();
    }

    private void FixedUpdate()
    {
        playerMovement.MoveLogic();
    }

    private void LateUpdate()
    {
        playerMovement.CamLogic();
    }


}
