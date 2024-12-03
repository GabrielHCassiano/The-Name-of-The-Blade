using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputs : MonoBehaviour
{
    [SerializeField] private Vector2 inputDirection;
    [SerializeField] private bool inputJump, inputAttack, inputDefese, inputRun, inputDash, inputAim, inputInteract, inputRecover, inputPause;
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

    public void OnInputDefese(InputAction.CallbackContext callbackContext)
    {
        inputDefese = callbackContext.action.triggered;
    }

    public void OnInputRun(InputAction.CallbackContext callbackContext)
    {
        inputRun = callbackContext.action.triggered;
    }
    

    public void OnInputDash(InputAction.CallbackContext callbackContext)
    {
        inputDash = callbackContext.action.triggered;
    }

    public void OnInputAim(InputAction.CallbackContext callbackContext)
    {
        inputAim = callbackContext.action.triggered;
    }

    public void OnInputInteract(InputAction.CallbackContext callbackContext)
    {
        inputInteract = callbackContext.action.triggered;
    }

    public void OnInputRecover(InputAction.CallbackContext callbackContext)
    {
        inputRecover = callbackContext.action.triggered;
    }
    public void OnInputPause(InputAction.CallbackContext callbackContext)
    {
        inputPause = callbackContext.action.triggered;
    }

    public void OnMousePosition(InputAction.CallbackContext callbackContext)
    {
        mousePosition = callbackContext.action.ReadValue<Vector2>();
    }

    public void ResetAllInputs()
    {
        inputDirection = Vector2.zero;
        inputJump = false;
        inputAttack = false;
        inputDefese = false; 
        inputRun = false;
        inputDash = false;
        inputAim = false;
        inputInteract = false;
        inputRecover = false;
        inputPause = false;
        mousePosition = Vector2.zero;
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

    public bool InputDefese
    {
        get { return inputDefese; }
        set { inputDefese = value; }
    }

    public bool InputRun
    {
        get { return inputRun; }
        set { inputRun = value; }
    }

    public bool InputDash
    {
        get { return inputDash; }
        set { inputDash = value; }
    }

    public bool InputAim
    {
        get { return inputAim; }
        set { inputAim = value; }
    }
    public bool InputInteract
    {
        get { return inputInteract; }
        set { inputInteract = value; }
    }

    public bool InputRecover
    {
        get { return inputRecover; }
        set { inputRecover = value; }
    }
    public bool InputPause
    {
        get { return inputPause; }
        set { inputPause = value; }
    }

    public Vector2 MousePosition
    {
        get { return mousePosition; }
        set { mousePosition = value; }
    }

}
