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

    [SerializeField] private Vector3 test;

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
        direction.y = 0;
        rb.velocity = direction * speed;
    }

    public void JumpLogic()
    {

    }

    public void CamLogic()
    {


        transform.eulerAngles = Vector3.up * Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;


        mouseRotation.x += playerInputs.MousePosition.x;
        mouseRotation.y += playerInputs.MousePosition.y;

        mouseRotation.y = Mathf.Clamp(mouseRotation.y, -5, 10);

        camPoint.transform.rotation = Quaternion.Euler(mouseRotation.y, mouseRotation.x, 0);
    }

    public void AnimLogic()
    {
        animator.SetFloat("Horizontal", playerInputs.InputDirection.x);
        animator.SetFloat("Vertical", playerInputs.InputDirection.y);
    }
}
