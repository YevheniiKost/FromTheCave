using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public bool IsGamePaused = false;
    public static GameManager Instance;

    private float _levelCompleteTime;
    private PlayerController _player;

    private void Awake()
    {
        SubscribeOnEvents();
        DontDestroyOnLoad(this.gameObject);

        if (Instance != null)
            Destroy(this.gameObject);
        else
            Instance = this;
    }

    private void Start()
    {
        AudioManager.Instance.PlayMusic(MusicType.MainMenu);
    }

    private void Update()
    {
        ProcessEscButton();
    }

    private void ProcessEscButton()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && SceneManager.GetActiveScene().buildIndex == 1)
        {
            if (!IsGamePaused)
                EventAggregator.RaiseOnPauseGameEvent();
            else
                EventAggregator.RaiseOnUnpaseGameEvent();
        }
    }

    private void OnDestroy()
    {
        UnsubscribeToEvents();
    }

    #region HandleEvents
    private void SubscribeOnEvents()
    {
        EventAggregator.OnPlayerDeath += ProcessPlayerDeath;
        EventAggregator.OnRestartLevel += RestartLevel;
        EventAggregator.OnFinishLevel += FinishLevel;
        EventAggregator.OnStartGame += StartGame;
        EventAggregator.OnGamePause += PauseGame;
        EventAggregator.OnGameUnpause += UnpauseGame;
        EventAggregator.OnReturnToMainMenu += ReturnToMainMenu;
    }

    private void UnsubscribeToEvents()
    {
        EventAggregator.OnPlayerDeath -= ProcessPlayerDeath;
        EventAggregator.OnRestartLevel -= RestartLevel;
        EventAggregator.OnFinishLevel -= FinishLevel;
        EventAggregator.OnStartGame -= StartGame;
        EventAggregator.OnGamePause -= PauseGame;
        EventAggregator.OnGameUnpause -= UnpauseGame;
        EventAggregator.OnReturnToMainMenu -= ReturnToMainMenu;
    }

    #endregion

    private void PauseGame()
    {
        IsGamePaused = true;
        Time.timeScale = 0;
    }

    private void UnpauseGame()
    {
        IsGamePaused = false;
        Time.timeScale = 1;
    }

    private void StartGame()
    {
        SceneManager.LoadScene(2);
        AudioManager.Instance.PlayMusic(MusicType.Game);
    }

    private void FinishLevel()
    {
        DisablePlayerControls();
        _player = FindObjectOfType<PlayerController>();
        UIManager.Instance.YouWinWindow.SetWinWindow(_player.CurrentScores, Time.timeSinceLevelLoad);
    }

    private void RestartLevel()
    {
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene(1);
    }

    private void ReturnToMainMenu()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1;
        AudioManager.Instance.PlayMusic(MusicType.MainMenu);
    }

    private void ProcessPlayerDeath()
    {
       // SceneManager.LoadScene(0);
    }

    private void DisablePlayerControls()
    {
        _player = FindObjectOfType<PlayerController>();
        _player.enabled = false;
        _player.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
    }
}
