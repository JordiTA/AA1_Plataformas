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
        //IDLE - WALK - RUN - WALKCROUCHED
        animator.SetFloat("velocity", InputManager._INPUT_MANAGER.GetMovementValue());

        //JUMP - AIR
        animator.SetFloat("VerticalSpeed", characterMovement.GetVerticalSpeed());
        animator.SetBool("isGrounded", characterMovement.GetIsAir());

        //CROUCH - WALKCROUCHED
        animator.SetBool("isCrouched", InputManager._INPUT_MANAGER.GetCrouchButton());
    }
}
