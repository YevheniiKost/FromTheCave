using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public delegate void StrikeHandler();

    public event StrikeHandler Strike;

    [SerializeField] private int _noWeaponDamage = 10;
    [SerializeField] private int _swordDamage = 50;
    [SerializeField] private int _bowDamage = 30;
    [SerializeField] private float _kickAttackRange = .3f;
    [SerializeField] private float _swordAttackRange = 1f;
    [SerializeField] private float _meeleAttackRate = 1f; 
    [SerializeField] private LayerMask _enemyLayer;
    [SerializeField] private Transform _attackPoint;
    [SerializeField] private AnimatorOverrideController _swordAnimController;

    public bool IsHasSword;
    public bool IsHasBow;

    private RuntimeAnimatorController _controllerBase;
    private Animator animator;
    private PlayerInput input;
    private float _nexAttackTime = 0f;

    

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        input = GetComponent<PlayerInput>();
    }
    private void Start()
    {
        _controllerBase = animator.runtimeAnimatorController;
    }

    private void Update()
    {
        UpdateAnimation();
        ProcessStrikes();
    }

    private void ProcessStrikes()
    {
        float currentAttacRange = IsHasSword ? _swordAttackRange : _kickAttackRange;
        int currentDamage = IsHasSword ? _swordDamage : _noWeaponDamage;

        if (input.Strike && Time.time >= _nexAttackTime)
        {
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(_attackPoint.position, currentAttacRange, _enemyLayer);

            foreach (var enemy in hitEnemies)
            {
                if(enemy.TryGetComponent( out EnemyHealth enemyHit))
                {
                    enemyHit.GetHit(currentDamage);
                }
            }

            _nexAttackTime = Time.time + 1f / _meeleAttackRate;
            Strike?.Invoke();
        }
    }

    private void UpdateAnimation()
    {
        if (IsHasSword)
        {
            animator.runtimeAnimatorController = _swordAnimController;
        }
        else if (!IsHasSword)
        {
            animator.runtimeAnimatorController = _controllerBase;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(_attackPoint.position, _swordAttackRange);
    }
}
