using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerHealth : MonoBehaviour, ISaveState
{
    public delegate void Action();

    public event Action OnHit;

    [SerializeField] private int _startHealth;
    [SerializeField] private int _maxHealth;

    public bool IsCharacterDead = false;

    private int _currentPlayerHealth;

    public void Save()
    {
        PlayerPrefs.SetInt("PlayerHealth", _currentPlayerHealth);
        var jsonIsDead = JsonUtility.ToJson(IsCharacterDead);
        PlayerPrefs.SetString("IsPlayerDead", jsonIsDead);
    }

    public void Load()
    {
        _currentPlayerHealth = PlayerPrefs.GetInt("PlayerHealth");
        IsCharacterDead = JsonUtility.FromJson<bool>(PlayerPrefs.GetString("IsPlayerDead"));
        GameEvents.RaiseOnChangeHealthEvent(_currentPlayerHealth);
    }

    public void ModifyHealth(int amount)
    {
        if (!IsCharacterDead)
        {
            if (!GetComponent<PlayerCombat>().IsBlockUp)
            {
                if (_currentPlayerHealth == _maxHealth && amount > 0)
                    return;

                if (amount > _maxHealth - _currentPlayerHealth)
                    amount = _maxHealth - _currentPlayerHealth;

                if (_currentPlayerHealth + amount <= 0)
                    StartDeathSequence();

                if (amount < 0)
                {
                    AudioManager.Instance.PlaySFX(SoundsFx.PlayerGetHit);
                    OnHit?.Invoke();
                }

                _currentPlayerHealth += amount;
                GameEvents.RaiseOnChangeHealthEvent(_currentPlayerHealth);
            }
            else
                AudioManager.Instance.PlaySFX(SoundsFx.ShieldHit);
        }
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
        _currentPlayerHealth = _startHealth;
        GameEvents.RaiseOnChangeHealthEvent(_currentPlayerHealth);
    }

    private void Update()
    {
        if(_currentPlayerHealth <= 0 && !IsCharacterDead)
        {
            _currentPlayerHealth = 0;
            StartDeathSequence();
        }
    }

    private void StartDeathSequence()
    {
        IsCharacterDead = true;
        AudioManager.Instance.PlaySFX(SoundsFx.CharacterDead);
        StartCoroutine(ExecuteAfterDelay(1f));
    }

    private IEnumerator ExecuteAfterDelay(float time)
    {
        yield return new WaitForSeconds(time);
        GameEvents.RaiseOnPlayerDeathEvent();
    }
}
