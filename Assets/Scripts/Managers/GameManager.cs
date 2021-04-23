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
        DontDestroyOnLoad(this.gameObject);
    }

    private void OnDestroy()
    {
        UnsubscribeToEvents();
    }
    private void SubscribeOnEvents()
    {
        EventAggregator.OnPlayerDeath += ProcessPlayerDeath;
        EventAggregator.OnRestartLevel += RestartLevel;
        EventAggregator.OnFinishLevel += FinishLevel;
        EventAggregator.OnStartGame += StartGame;
    }

    private void UnsubscribeToEvents()
    {
        EventAggregator.OnPlayerDeath -= ProcessPlayerDeath;
        EventAggregator.OnRestartLevel -= RestartLevel;
        EventAggregator.OnFinishLevel -= FinishLevel;
        EventAggregator.OnStartGame -= StartGame;
    }

    private void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    private void FinishLevel()
    {
        DisablePlayerControls();
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

    private void DisablePlayerControls()
    {
        var player = FindObjectOfType<PlayerController>();
        player.enabled = false;
        player.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
    }
}
