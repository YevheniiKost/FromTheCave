using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int _startHealth;
    [SerializeField] private int _maxHealth;

    public bool IsCharacterDead;

    private int _currentPlayerHealth;

    public void ModifyHealth(int amount)
    {
        if (_currentPlayerHealth == _maxHealth && amount > 0)
            return;

        if (amount > _maxHealth - _currentPlayerHealth)
            amount = _maxHealth - _currentPlayerHealth;

        if (_currentPlayerHealth + amount <= 0)
            StartDeathSequence();

        _currentPlayerHealth += amount;
    }

    public void KillImmidiately()
    {
        if (!IsCharacterDead)
        {
            _currentPlayerHealth = 0;
            StartDeathSequence();
        }
    }

    private void Start()
    {
        IsCharacterDead = false;
    }

    private void StartDeathSequence()
    {
        IsCharacterDead = true;
    }
}
