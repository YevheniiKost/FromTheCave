using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimation : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private AnimatorOverrideController _overrideController;

    private int _isWalkingParamID;
    private int _attackParamID;
    private int _hitParamID;
    private int _isDeadParamID;

    private Rigidbody2D _rb;
    

    private void Awake()
    {
        GetComponent<EnemyHealth>().OnGetHit += PlayHitAnimation;
        GetComponent<EnemyMovement>().OnAttack += PlayAttackAnimation;

        _rb = GetComponent<Rigidbody2D>();

        _isWalkingParamID = Animator.StringToHash("IsWalking");
        _attackParamID = Animator.StringToHash("Attack");
        _hitParamID = Animator.StringToHash("Hit");
        _isDeadParamID = Animator.StringToHash("IsDead");
    }

    private void Start()
    {
        if(_overrideController != null)
        {
            _animator.runtimeAnimatorController = _overrideController;
        }
    }

    private void PlayAttackAnimation()
    {
        _animator.SetTrigger(_attackParamID);
    }

    private void PlayHitAnimation()
    {
        _animator.SetTrigger(_hitParamID);
    }

    private void Update()
    {
        CatchTheVelocity();
        _animator.SetBool(_isDeadParamID, GetComponent<EnemyHealth>().IsDead);
    }

    private void CatchTheVelocity()
    {
        if(Mathf.Abs(_rb.velocity.x) > 0 || Mathf.Abs(_rb.velocity.y) > 0)
        {
            _animator.SetBool(_isWalkingParamID, true);
        }
        else
        {
            _animator.SetBool(_isWalkingParamID, false);
        }
    }
}
