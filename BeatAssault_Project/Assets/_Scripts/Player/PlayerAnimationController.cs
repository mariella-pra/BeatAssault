using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{

    public Animator animator;
    private PlayerVariables _variables;
    public CharacterController characterController;
    float smoothSpeed= 0f;
    float smoothTime = 0.1f;
    
    void Start()
    {
        animator.GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
        _variables = GetComponent<PlayerVariables>();
        // PlayDeathAnimation();
    }

    // private void OnEnable()
    // {
    //     _variables.playerEvents?.AddEvent("playerDeath", PlayDeathAnimation);
    // }
    // private void OnDestroy()
    // {
    //     _variables.playerEvents?.RemoveEvent("playerDeath", PlayDeathAnimation);
    // }

    void Update()
    {
        //float velocity = new Vector3(characterController.velocity.x, 0, characterController.velocity.z).magnitude;
        Vector3 velocityVector = characterController.velocity;
        float velocity = velocityVector.magnitude;

        float targetSpeed = velocity > 0.1f ? 1f : 0f;
        smoothSpeed = Mathf.Lerp(smoothSpeed, targetSpeed, Time.deltaTime / smoothTime);

        animator.SetFloat("speed", smoothSpeed);
    }

    public void PlayDeathAnimation()
    {
        // animator.applyRootMotion = true;
        animator.SetBool("death", true);
    }
}
