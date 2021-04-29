using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent (typeof(Rigidbody2D)), RequireComponent (typeof(CapsuleCollider2D))]
public class EnemyHealth : MonoBehaviour, ISaveState
{
    public delegate void Action();

    public event Action OnGetHit;
    public event Action<float> OnHealthPctChanged;

    [SerializeField] private float _startHealth;

    public bool IsDead;

    private float _currentHealth;
    private string _enemyName;

    public void Save()
    {
        PlayerPrefs.SetFloat($"Health-{_enemyName}", _currentHealth);
        PlayerPrefs.SetInt($"IsDead-{_enemyName}", IsDead ? 1 : 0);
    }

    public void Load()
    {
        _currentHealth = PlayerPrefs.GetFloat($"Health-{_enemyName}");
        IsDead = PlayerPrefs.GetInt($"IsDead-{_enemyName}") == 1;

        if (!IsDead)
            CallHealthChange();
    }

    public void GetHit(int amount)
    {
        if (!IsDead)
        {
            _currentHealth -= amount;

            if (_currentHealth <= 0)
                StartDeath();

            CallHealthChange();
            OnGetHit?.Invoke();

            
        }
    }

    private void CallHealthChange()
    {
        float currentHealthPct = (float)_currentHealth / (float)_startHealth;
        OnHealthPctChanged?.Invoke(currentHealthPct);
    }

    private void Awake()
    {
        _enemyName = transform.parent.name;
    }

    void Start()
    {
        _currentHealth = _startHealth;
        IsDead = false;
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
