using System.Collections;
using System.Collections.Generic;
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

    private void Awake()
    {
        if (_INPUT_MANAGER != null && _INPUT_MANAGER != this)
        {
            Destroy(_INPUT_MANAGER); //Destruir si ya existe uno
        }
        else
        {
            //Activar Input Action
            playerInputs = new InputActions();
            playerInputs.Character.Enable();

            playerInputs.Character.Jump.performed += JumpButtonPressed; // ASI SE DERIVA EN InputSystem a una FUNCION
            playerInputs.Character.Move.performed += leftAxisUpdate;

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

    private void JumpButtonPressed(InputAction.CallbackContext context)
    {
        timeSinceJumpPressed = 0f;
    }
    private void leftAxisUpdate(InputAction.CallbackContext context)
    {
        leftAxisValue = context.ReadValue<Vector2>();

        //Debug.Log("Magnitude: " + leftAxisValue.magnitude);
        //Debug.Log("Normalized: " + leftAxisValue.normalized);
    }

    public bool GetSouthButtonPressed()
    {
        return this.timeSinceJumpPressed == 0f;
    }
    public float TimeSinceSouthButtonPressed()
    {
        return this.timeSinceJumpPressed;
    }
}
