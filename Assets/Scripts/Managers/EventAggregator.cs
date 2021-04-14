using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventAggregator 
{
    public delegate void GameEvent();
    public delegate void GameEventInt(int someInt);

    public static event GameEventInt OnChangeScore;
    public static event GameEventInt OnChangeHealth;
    public static event GameEvent OnPlayerDeath;
    public static event GameEvent OnSaveGame;
    public static event GameEvent OnLoadGame;
    public static event GameEvent OnRestartLevel;

    public static void RaiseOnRestartLevelEvent()
    {
        OnRestartLevel?.Invoke();
    }
    public static void RaiseOnSaveGameEvent()
    {
        OnSaveGame?.Invoke();
    }
    public static void RaiseOnLoadGameEvent()
    {
        OnLoadGame?.Invoke();
    }
    public static void RaiseOnPlayerDeathEvent()
    {
        OnPlayerDeath?.Invoke();
    }

    public static void RaiseOnChangeScoreEvent(int score)
    {
        OnChangeScore?.Invoke(score);
    }

    public static void RaiseOnChangeHealthEvent(int currentHealth)
    {
        OnChangeHealth?.Invoke(currentHealth);
    }
}


