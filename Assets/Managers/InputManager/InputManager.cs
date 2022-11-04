using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    //PUBLIC
    public static InputManager _INPUT_MANAGER; //SINGELTON

    //PRIVATE
    private InputActions playerInputs;

    //VARIABLES
        //JUMP
    private float timeSinceJumpPressed = 0f;
        //MOVE
    private Vector2 leftAxisValue = Vector2.zero;
        //LOOK MOUSE/JOYSTICK RIGHT
    private Vector2 mouseXY = Vector2.zero;
    private float mouseX = 0f;
    private float mouseY = 0f;
        //CROUCH
    private bool pressingCrouchButton = false;
        //HAT
    private float pressingHatButton = 0f;
    private void Awake()
    {
        if (_INPUT_MANAGER != null && _INPUT_MANAGER != this)
        {
            Destroy(_INPUT_MANAGER); //Destruir si ya existe uno
        }
        else
        {
            //Activar input Actions
            playerInputs = new InputActions();
            playerInputs.Character.Enable();

            playerInputs.Character.Move.performed += leftAxisUpdate;
            playerInputs.Character.Jump.performed += JumpButtonPressed;
            playerInputs.Character.Look.performed += LookUpdate;
            playerInputs.Character.Crouch.performed += timeCrouchButtonPressed;
            playerInputs.Character.Crouch.canceled += timeCrouchButtonReleased;
            playerInputs.Character.Hat.performed += HatButtonPressed;
            
            //Dont destroy on load para cambiar de escenas
            _INPUT_MANAGER = this;
            DontDestroyOnLoad(this);
        }
    }

    private void Update()
    {
        timeSinceJumpPressed += Time.deltaTime;
        pressingHatButton += Time.deltaTime;
        InputSystem.Update();
    }
        //MOVE
    private void leftAxisUpdate(InputAction.CallbackContext context)
    {
        leftAxisValue = context.ReadValue<Vector2>();
    }
        //JUMP
    private void JumpButtonPressed(InputAction.CallbackContext context)
    {
        timeSinceJumpPressed = 0f;
    }
        //LOOK
    private void LookUpdate(InputAction.CallbackContext context)
    {
        mouseXY = context.ReadValue<Vector2>();
        mouseX = mouseXY.x;
        mouseY = mouseXY.y;
    }
        //CROUCH
    private void timeCrouchButtonPressed(InputAction.CallbackContext context)
    {
        pressingCrouchButton = true;
    }
    private void timeCrouchButtonReleased(InputAction.CallbackContext context)
    {
        pressingCrouchButton = false;
    }
        //HAT
    private void HatButtonPressed(InputAction.CallbackContext context)
    {
        pressingHatButton = 0f;
    }


    //GET FUNCTIONS
        //MOVE
    public Vector2 GetMovement()
    {
        return leftAxisValue;
    }
    public float GetMovementValue()
    {
        float value = leftAxisValue.x * leftAxisValue.x + leftAxisValue.y * leftAxisValue.y;
        return value;
    }
        //JUMP
    public bool GetJumpButtonPressed()
    {
        return this.timeSinceJumpPressed == 0f;
    }
        //LOOK
    public float GetMouseX()
    {
        return mouseX;
    }
    public float GetMouseY()
    {
        return mouseY;
    }
        //CROUCH
    public bool GetCrouchButton()
    {
        return this.pressingCrouchButton;
    }
        //HAT
    public bool GetHatButtonPressed()
    {
        return this.pressingHatButton == 0;
    }
    public float GetTimeHatButton()
    {
        return this.pressingHatButton;
    }
}
