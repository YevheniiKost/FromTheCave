using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public delegate void Action();

    public event Action OnStrike;
    public event Action OnRangeStrike;

    [SerializeField] private int _noWeaponDamage = 10;
    [SerializeField] private int _swordDamage = 50;
    [SerializeField] private int _rangeDamage = 30;
    [SerializeField] private float _kickAttackRange = .3f;
    [SerializeField] private float _swordAttackRange = 1f;
    [SerializeField] private float _meeleAttackRate = 1f;
    [SerializeField] private float _rangeAttaclRate = 1f;
    [SerializeField] private LayerMask _enemyLayer;
    [SerializeField] private Transform _attackPoint;
    [SerializeField] private AnimatorOverrideController _swordAnimController;
    [SerializeField] private GameObject _projectilePrefab;

    public bool IsHasSword;
    public bool IsHasAxe;

    private RuntimeAnimatorController _controllerBase;
    private Animator animator;
    private PlayerInput input;
    private float _nextSwordAttackTime = 0f;
    private float _nextRangeAttackTime = 0f;

    public void GetSword()
    {
        if (!IsHasSword)
        {
            IsHasSword = true;
        }
    }

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
        ProcessRangeStrikes();
        _nextSwordAttackTime += Time.deltaTime;
        _nextRangeAttackTime += Time.deltaTime;
    }

    private void ProcessRangeStrikes()
    {
        if (input.RangeStrike && IsHasAxe && _nextRangeAttackTime >= _rangeAttaclRate)
        {
            var axe = Instantiate(_projectilePrefab, _attackPoint.position + Vector3.up * .5f, Quaternion.identity);
            axe.GetComponent<ProjectileAxe>().GetPlayerData(_rangeDamage, transform.localScale.x);

            _nextRangeAttackTime = 0;

            OnRangeStrike?.Invoke();
        }
    }

    private void ProcessStrikes()
    {
        float currentAttacRange = IsHasSword ? _swordAttackRange : _kickAttackRange;
        int currentDamage = IsHasSword ? _swordDamage : _noWeaponDamage;

        if (input.Strike && _nextSwordAttackTime >= _meeleAttackRate)
        {
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(_attackPoint.position, currentAttacRange, _enemyLayer);

            foreach (var enemy in hitEnemies)
            {
                if(enemy.TryGetComponent( out EnemyHealth enemyHit))
                {
                    enemyHit.GetHit(currentDamage);
                }
            }

            _nextSwordAttackTime = 0;
           
            OnStrike?.Invoke();
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
