using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private float _startHealth;

    private float _currentHealth;
    private bool _isDead;

    public void GetHit(int amount)
    {
        _currentHealth -= amount;
    }

    void Start()
    {
        _currentHealth = _startHealth;
        _isDead = false;
    }

    void Update()
    {
        if(_currentHealth <= 0)
        {
            StartDeath();
        }
    }

    private void StartDeath()
    {
        _isDead = true;
    }

    
}
