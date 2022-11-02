using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterMovement : MonoBehaviour
{
    private CharacterController characterController;

    [SerializeField]
    private float speed = 5f;

    [SerializeField]
    private float rotationSpeed = 10f;

    [SerializeField]
    private Camera Camera;

    //VARIABLES
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
        Jump();
    }
    private void Movement()
    {
        //MOVEMENT
        Vector2 movement = InputManager._INPUT_MANAGER.GetMovement();

        Vector3 MovementInput = Quaternion.Euler(0, Camera.transform.eulerAngles.y,0) * new Vector3(movement.x, 0, movement.y);
        Vector3 movementDirection = MovementInput.normalized;

        characterController.Move(movementDirection * speed * Time.deltaTime);

        //FACE CAMERA FORWARD
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

    
}
