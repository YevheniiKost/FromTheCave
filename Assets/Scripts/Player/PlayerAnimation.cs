using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField] private Animator animator;

    private Rigidbody2D rb;
    private PlayerController controller;

    private int _runningParamID;
    private int _jumpingParamID;
    private int _climbingParamID;
    private int _deathParamID;
    private int _hitParamID;
    private int _strikeParamID;

    private bool _isRunning;
    private bool _isJumping;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        controller = GetComponent<PlayerController>();
        GetComponent<PlayerHealth>().Hit += PlayHitAnimation;
        GetComponent<PlayerCombat>().Strike += PlayStrikeAnimation;
    }
    private void Start()
    {
        SetParametersID();
    }

    private void Update()
    {
        CatchTheVelocity();

        SetAnimatorParameters();
    }

    private void SetAnimatorParameters()
    {
        animator.SetBool(_climbingParamID, controller.IsClimbing);
        animator.SetBool(_runningParamID, _isRunning);
        animator.SetBool(_jumpingParamID, _isJumping);
        animator.SetBool(_deathParamID, GetComponent<PlayerHealth>().IsCharacterDead);
    }

    private void SetParametersID()
    {
        _runningParamID = Animator.StringToHash("IsRunning");
        _jumpingParamID = Animator.StringToHash("IsJumping");
        _climbingParamID = Animator.StringToHash("IsClimbing");
        _deathParamID = Animator.StringToHash("IsDead");
        _hitParamID = Animator.StringToHash("IsHit");
        _strikeParamID = Animator.StringToHash("Attack");
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

        if (GetComponent<PlayerHealth>().IsCharacterDead)
        {
            _isJumping = false;
            _isRunning = false;
        }
    }

    private void PlayStrikeAnimation()
    {
        animator.SetTrigger(_strikeParamID);
    }
    private void PlayHitAnimation()
    {
        animator.SetTrigger(_hitParamID);
    }
}
