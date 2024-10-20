using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody rb;
    private PlayerInputs playerInputs;
    private Animator animator;

    [SerializeField] private GameObject camPoint;

    private Vector3 direction;
    private Vector2 mouseRotation;
    [SerializeField] private float speed;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerInputs = GetComponent<PlayerInputs>();
        animator = GetComponentInChildren<Animator>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        AnimLogic();
        CamLogic();
        JumpLogic();
    }

    private void FixedUpdate()
    {
        MoveLogic();
    }

    public void MoveLogic()
    {
        direction = new Vector3(playerInputs.InputDirection.x, 0, playerInputs.InputDirection.y);
        direction = direction.normalized;
        direction = camPoint.transform.TransformDirection(direction);
        rb.velocity = direction * speed;
    }

    public void JumpLogic()
    {

    }

    public void CamLogic()
    {
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, camPoint.transform.eulerAngles.y, transform.eulerAngles.z);

        mouseRotation.x += playerInputs.MousePosition.x;
        mouseRotation.y += playerInputs.MousePosition.y;


        camPoint.transform.rotation = Quaternion.Euler(0, mouseRotation.x, 0);
    }

    public void AnimLogic()
    {
        animator.SetFloat("Horizontal", playerInputs.InputDirection.x);
        animator.SetFloat("Vertical", playerInputs.InputDirection.y);
    }
}
