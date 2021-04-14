using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    private void Awake()
    {
        SubscribeOnEvents();
    }

    private void OnDestroy()
    {
        UnsubscribeToEvents();
    }

    private void UnsubscribeToEvents()
    {
        EventAggregator.OnPlayerDeath -= ProcessPlayerDeath;
        EventAggregator.OnRestartLevel -= RestartLevel;
    }

    private void SubscribeOnEvents()
    {
        EventAggregator.OnPlayerDeath += ProcessPlayerDeath;
        EventAggregator.OnRestartLevel += RestartLevel;
    }


    private void RestartLevel()
    {
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene(0);
    }
    private void ProcessPlayerDeath()
    {
       // SceneManager.LoadScene(0);
    }
}
