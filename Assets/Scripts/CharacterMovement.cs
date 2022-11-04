using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.HID;
using UnityEngine.SceneManagement;

public class CharacterMovement : MonoBehaviour
{
    private CharacterController characterController;

    [SerializeField]
    private float normalSpeed = 5f;
    [SerializeField]
    private float maxSpeed = 8f;
    [SerializeField]
    private float crouchSpeed = 3f;

    [SerializeField]
    private float rotationSpeed = 10f;

    [SerializeField]
    private Camera Camera;
    

    //VARIABLES
    private string enemy = "Enemy";
    private string platform = "Platform";
    private RaycastHit rayCastHit;

    private Vector3 playerVelocity;
    private bool isGrounded = true;
    private float jumpCount = 0f;

    [SerializeField]
    private float jumpHeight = 1.0f;
    [SerializeField]
    private float gravity = -10f;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        Movement();
        if (Physics.Raycast(transform.position, -Vector3.up, out rayCastHit, 0.1f) && rayCastHit.collider.gameObject.tag == platform)
            PlatformJump();
        else
            Jump();
    }
    private void Movement()
    {
        //MOVEMENT
        Vector2 movement = InputManager._INPUT_MANAGER.GetMovement();

        Vector3 MovementInput = Quaternion.Euler(0, Camera.transform.eulerAngles.y,0) * new Vector3(movement.x, 0, movement.y);
        Vector3 movementDirection = MovementInput.normalized;

        if (InputManager._INPUT_MANAGER.GetCrouchButton())
        {
            characterController.Move(movementDirection * crouchSpeed * Time.deltaTime); // WALK SPEED
        }
        else if (InputManager._INPUT_MANAGER.GetMovementValue() < 0.4f)
        {
            characterController.Move(movementDirection * normalSpeed * Time.deltaTime); // WALK SPEED
        }
        else
        {
            characterController.Move(movementDirection * maxSpeed * Time.deltaTime); // RUN SPEED
        }

        //FACE CAMERA to MOVE
        if (movementDirection != Vector3.zero)
        {
            Quaternion rotation = Quaternion.LookRotation(movementDirection, Vector3.up);

            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
        }
    }
    private void Jump()
    {
        isGrounded = Physics.Raycast(transform.position,-Vector3.up, 0.1f);
        if (isGrounded && playerVelocity.y < 0)
        {
            playerVelocity.y = 0;
            jumpCount = 0f;
        }

        //JUMP
        if (InputManager._INPUT_MANAGER.GetJumpButtonPressed() && jumpCount < 3)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravity);
            jumpCount++;
        }

        playerVelocity.y += gravity * Time.deltaTime;
        characterController.Move(playerVelocity * Time.deltaTime);
    }    
    private void PlatformJump()
    {
        playerVelocity.y += Mathf.Sqrt(jumpHeight * -7.0f * gravity);

        playerVelocity.y += gravity * Time.deltaTime;
        characterController.Move(playerVelocity * Time.deltaTime);
    }
    public bool GetIsAir()
    {
        return Physics.Raycast(transform.position, -Vector3.up, 1f);
    }
    public float GetVerticalSpeed()
    {
        
        return playerVelocity.y;
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit != null && hit.gameObject.tag == enemy)
        {
            SceneLoadingManager._SCENE_MANAGER.Lost();
        }
    }
    
}