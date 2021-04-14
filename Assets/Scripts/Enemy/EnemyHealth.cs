using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour, ISaveState
{
    public delegate void Action();

    public event Action OnGetHit;

    [SerializeField] private float _startHealth;

    public bool IsDead;

    private float _currentHealth;

    public void Save()
    {
        PlayerPrefs.SetFloat($"Health-{gameObject.name}", _currentHealth);
        PlayerPrefs.SetInt($"IsDead-{gameObject.name}", IsDead ? 1 : 0);
    }

    public void Load()
    {
        _currentHealth = PlayerPrefs.GetFloat($"Health-{gameObject.name}");
        IsDead = PlayerPrefs.GetInt($"IsDead-{gameObject.name}") == 1;
    }

    public void GetHit(int amount)
    {
        if (!IsDead)
        {
            _currentHealth -= amount;
            OnGetHit?.Invoke();
        }
    }

    void Start()
    {
        _currentHealth = _startHealth;
        IsDead = false;
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
        IsDead = true;
        GetComponent<EnemyMovement>().enabled = false;
        GetComponent<Rigidbody2D>().gravityScale = 0;
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        GetComponent<CapsuleCollider2D>().enabled = false;
    }

   
}
