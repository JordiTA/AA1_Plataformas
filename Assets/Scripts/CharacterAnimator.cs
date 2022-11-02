using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimator : MonoBehaviour
{
    private Animator animator;
    
    private CharacterMovement characterMovement;

    private void Awake()
    {
        animator = GetComponent<Animator>();

        characterMovement = GetComponent<CharacterMovement>();
    }
    // Update is called once per frame
    void Update()
    {
        //IDLE - WALK - RUN
        animator.SetFloat("velocity", InputManager._INPUT_MANAGER.GetMovementValue());

        //JUMP
        animator.SetBool("isJumping", InputManager._INPUT_MANAGER.GetJumpButtonPressed());

        //CROUCH - WALKCROUCHED

    }
}
