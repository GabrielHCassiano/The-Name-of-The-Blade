using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputs : MonoBehaviour
{
    [SerializeField] private Vector2 inputDirection;
    [SerializeField] private bool inputJump, inputAttack;
    [SerializeField] private Vector2 mousePosition;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnInputDirection(InputAction.CallbackContext callbackContext)
    {
        inputDirection = callbackContext.action.ReadValue<Vector2>();
    }

    public void OnInputJump(InputAction.CallbackContext callbackContext)
    {
        inputJump = callbackContext.action.triggered;
    }

    public void OnInputAttack(InputAction.CallbackContext callbackContext)
    {
        inputAttack = callbackContext.action.triggered;
    }


    public void OnMousePosition(InputAction.CallbackContext callbackContext)
    {
        mousePosition = callbackContext.action.ReadValue<Vector2>();
    }


    public Vector2 InputDirection
    { 
        get {  return inputDirection; }
        set { inputDirection = value; }
    }

    public bool Inputjump
    {
        get { return inputJump; }
        set { inputJump = value; }
    }

    public bool InputAttack
    {
        get { return inputAttack; }
        set { inputAttack = value; }

    }

    public Vector2 MousePosition
    {
        get { return mousePosition; }
        set { mousePosition = value; }
    }

}
