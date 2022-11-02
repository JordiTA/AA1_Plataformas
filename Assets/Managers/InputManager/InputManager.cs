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
    private float timeSinceJumpPressed = 0f;
    
    private Vector2 leftAxisValue = Vector2.zero;
    
    private Vector2 mouseXY = Vector2.zero;
    private float mouseX = 0f;
    public float mouseY = 0f;

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

            //Dont destroy on load para cambiar de escenas
            _INPUT_MANAGER = this;
            DontDestroyOnLoad(this);
        }
    }

    private void Update()
    {
        timeSinceJumpPressed += Time.deltaTime;
        InputSystem.Update();
    }

    private void leftAxisUpdate(InputAction.CallbackContext context)
    {
        leftAxisValue = context.ReadValue<Vector2>();
    }
    private void JumpButtonPressed(InputAction.CallbackContext context)
    {
        timeSinceJumpPressed = 0f;
    }
    private void LookUpdate(InputAction.CallbackContext context)
    {
        mouseXY = context.ReadValue<Vector2>();
        mouseX = mouseXY.x;
        mouseY = mouseXY.y;
    }
    //GET FUNCTIONS
    public Vector2 GetMovement()
    {
        return leftAxisValue;
    }
    public bool GetJumpButtonPressed()
    {
        return this.timeSinceJumpPressed == 0f;
    }
    public float GetMouseX()
    {
        return mouseX;
    }
    public float GetMouseY()
    {
        return mouseY;
    }
    public float GetMovementValue()
    {
        float value = leftAxisValue.x * leftAxisValue.x + leftAxisValue.y * leftAxisValue.y;
        return value;
    }
}
