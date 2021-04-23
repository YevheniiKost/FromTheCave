using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public bool IsGamePaused = false;

    private void Awake()
    {
        SubscribeOnEvents();
        DontDestroyOnLoad(this.gameObject);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && SceneManager.GetActiveScene().buildIndex == 1)
        {
            if(!IsGamePaused)
            EventAggregator.RaiseOnPauseGameEvent();
            else
            {
                UnPauseGame();
            }
        }
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
        EventAggregator.OnGamePause += PauseGame;
    }

    private void UnsubscribeToEvents()
    {
        EventAggregator.OnPlayerDeath -= ProcessPlayerDeath;
        EventAggregator.OnRestartLevel -= RestartLevel;
        EventAggregator.OnFinishLevel -= FinishLevel;
        EventAggregator.OnStartGame -= StartGame;
        EventAggregator.OnGamePause -= PauseGame;
    }

    private void PauseGame()
    {
        IsGamePaused = true;
        Time.timeScale = 0;
    }

    private void UnPauseGame()
    {
        IsGamePaused = false;
        Time.timeScale = 1;
        UIManager.Instance.PauseWindow.gameObject.SetActive(false);
    }

    private void StartGame()
    {
        SceneManager.LoadScene(2);
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
