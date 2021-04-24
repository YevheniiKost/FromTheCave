using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public delegate void Atcion();

    public event Atcion OnAttack;

    [SerializeField] private float _movingSpeed = 1f;
    [SerializeField] private int _attackDamage = 1;
    [SerializeField] private float _attackRate = .5f;

    [SerializeField] private float _meeleAttackDistance = 1f;
    [SerializeField] private float _chasingDistance = 5f;

    [SerializeField] private Transform _attackPoint;
    [SerializeField] private Transform _patrollingPointA;
    [SerializeField] private Transform _patrollingPointB;
    [SerializeField] private float _waypointTolerance;
    [SerializeField] private LayerMask _playerMask;

    [SerializeField] private float _playerDetectionOffset = 1f;
    [SerializeField] private bool _drawDebugRaycasts = true;

    private Transform _currentWaypoint;
    private EnemyState _currentState;
    private Rigidbody2D _rb;
    private PlayerHealth _player;
    private int _direction = 1;
    private float _originalScale;
    private float _timeToNextAttack;

    // Animation event
    public void Attack()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(_attackPoint.position, _meeleAttackDistance);

        foreach (var enemy in hitEnemies)
        {
            if (enemy.TryGetComponent(out PlayerHealth playerHit))
            {
                playerHit.ModifyHealth(-_attackDamage);
                AudioManager.Instance.PlaySFX(SoundsFx.EnemyStrike);
            }
        }
    }

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _player = FindObjectOfType<PlayerHealth>();
    }

    private void Start()
    {
        _originalScale = transform.localScale.x;

        _currentWaypoint = _patrollingPointA;

        _currentState = EnemyState.Patroling;
    }

    private void Update()
    {
        CheckDistanceToPlayer();

        switch (_currentState)
        {
            case EnemyState.Patroling:
                Patrolling();
                break;
            case EnemyState.ChasePlayer:
                ChaseAndAttackPlayer();
                break;
            default:
                _currentState = EnemyState.Idle;
                    break;
        }
        _timeToNextAttack += Time.deltaTime;
    }

    private void Patrolling()
    {
        if(_currentWaypoint == _patrollingPointA)
            if(Vector3.Distance(_patrollingPointA.transform.position, transform.position) <= _waypointTolerance )
            {
                _currentWaypoint = _patrollingPointB;
                MoveToWaypoint(_currentWaypoint);
            } else
            {
                MoveToWaypoint(_currentWaypoint);
            }
        if (_currentWaypoint == _patrollingPointB)
        {
            if (Vector3.Distance(_patrollingPointB.transform.position, transform.position) <= _waypointTolerance && _currentWaypoint == _patrollingPointB)
            {
                _currentWaypoint = _patrollingPointA;
                MoveToWaypoint(_currentWaypoint);
            }
            else
            {
                MoveToWaypoint(_currentWaypoint);
            }
        }
    }

    private void MoveToWaypoint(Transform patrollingPoint)
    {
        MoveToTarget(new Vector2(patrollingPoint.position.x, _patrollingPointB.position.y));
    }

    private void CheckDistanceToPlayer()
    {
        RaycastHit2D leftHit = Raycast(new Vector2(0f, _playerDetectionOffset), Vector2.left, _chasingDistance, _playerMask);
        RaycastHit2D rightHit = Raycast(new Vector2(0f, _playerDetectionOffset), Vector2.right, _chasingDistance, _playerMask);

        if (leftHit || rightHit)
        {
            _currentState = EnemyState.ChasePlayer;
        } else
        {
            _currentState = EnemyState.Patroling;
        }
    }

    private void ChaseAndAttackPlayer()
    {
        if(Vector3.Distance(_player.transform.position, transform.position) > _meeleAttackDistance)
        {
            MoveToTarget(new Vector2(_player.transform.position.x, _player.transform.position.y));
        }
        else
        {
            ProcessAttack();
        }
    }

    private void ProcessAttack()
    {
       
        if(_timeToNextAttack >= _attackRate)
        {
            OnAttack?.Invoke();
            
            _timeToNextAttack = 0;
        }

    }

  

    private void MoveToTarget(Vector2 target)
    {
        Vector2 targetDirection = target - new Vector2(transform.position.x, transform.position.y);
        targetDirection.y = 0f;
        _rb.velocity = targetDirection.normalized * _movingSpeed;
        if (targetDirection.x * _direction < 0f)
            {
                FlipCharacterDirection();
            }
    }

    private void FlipCharacterDirection()
    {
        _direction *= -1;

        Vector3 scale = transform.localScale;

        scale.x = _originalScale * _direction;

        transform.localScale = scale;
    }

    private RaycastHit2D Raycast(Vector2 offset, Vector2 rayDirection, float length, LayerMask mask)
    {
        Vector2 pos = transform.position;

        RaycastHit2D hit = Physics2D.Raycast(pos + offset, rayDirection, length, mask);

        if (_drawDebugRaycasts)
        {
            Color color = hit ? Color.red : Color.green;
            Debug.DrawRay(pos + offset, rayDirection * length, color);
        }
        return hit;
    }
}

public enum EnemyState
{
    Patroling, 
    Idle,
    ChasePlayer
}
