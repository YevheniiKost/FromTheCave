using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float _movingSpeed = 1f;
    [SerializeField] private int _attackDamage = 10;

    [SerializeField] private float _meeleAttackDistance = 1f;
    [SerializeField] private float _chasingDistance = 5f;

    [SerializeField] private Transform _patrollingPointA;
    [SerializeField] private Transform _patrollingPointB;
    [SerializeField] private float _waypointTolerance;

    private Transform _currentWaypoint;
    private EnemyState _currentState;
    private Rigidbody2D _rb;
    private PlayerHealth _player;
    private int _direction = 1;
    private float _originalScale;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _player = FindObjectOfType<PlayerHealth>();
    }

    private void Start()
    {
        _originalScale = transform.localScale.x;

        _currentWaypoint = _patrollingPointA;
       // transform.position = _patrollingPointA.position;

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
        if(Vector3.Distance(_player.transform.position, transform.position) < _chasingDistance)
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
            Debug.Log("Attack");
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
}

public enum EnemyState
{
    Patroling, 
    Idle,
    ChasePlayer
}
