using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField] private Animator animator;

    private Rigidbody2D rb;
    private PlayerInput input;
    private PlayerController controller;

    private int _runningParamID;
    private int _jumpingParamID;
    private int _climbingParamID;

    private bool _isRunning;
    private bool _isJumping;



    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        input = GetComponent<PlayerInput>();
        controller = GetComponent<PlayerController>();
    }
    private void Start()
    {
        _runningParamID = Animator.StringToHash("IsRunning");
        _jumpingParamID = Animator.StringToHash("IsJumping");
        _climbingParamID = Animator.StringToHash("IsClimbing");
    }

    private void Update()
    {
        CatchTheVelocity();

        animator.SetBool(_climbingParamID, controller.IsClimbing);
        animator.SetBool(_runningParamID, _isRunning);
        animator.SetBool(_jumpingParamID, _isJumping);
    }

    private void CatchTheVelocity()
    {
        if(Mathf.Abs(rb.velocity.x) > 1)
        {
            _isRunning = true;
        } else { _isRunning = false; }

        if((Mathf.Abs(rb.velocity.y) > 1) || !controller.IsOnGround && !controller.IsClimbing)
        {
            _isJumping = true;
            _isRunning = false;
        } else
        {
            _isJumping = false;
        }

    }
}
