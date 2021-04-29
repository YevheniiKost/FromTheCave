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
        DontDestroyOnLoad(this.gameObject);

        if (Instance != null)
            Destroy(this.gameObject);
        else
            Instance = this;

        SubscribeOnEvents();
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
        if (Input.GetKeyDown(KeyCode.Escape) && SceneManager.GetActiveScene().name == GameConstants.SceneNames.MainLevelSceneName)
        {
            if (!IsGamePaused)
                GameEvents.RaiseOnPauseGameEvent();
            else
                GameEvents.RaiseOnUnpaseGameEvent();
        }
    }

    private void OnDestroy()
    {
        UnsubscribeToEvents();
    }

    #region HandleEvents
    private void SubscribeOnEvents()
    {
        GameEvents.OnPlayerDeath += ProcessPlayerDeath;
        GameEvents.OnRestartLevel += RestartLevel;
        GameEvents.OnFinishLevel += FinishLevel;
        GameEvents.OnStartGame += StartGame;
        GameEvents.OnGamePause += PauseGame;
        GameEvents.OnGameUnpause += UnpauseGame;
        GameEvents.OnReturnToMainMenu += ReturnToMainMenu;
    }

    private void UnsubscribeToEvents()
    {
        GameEvents.OnPlayerDeath -= ProcessPlayerDeath;
        GameEvents.OnRestartLevel -= RestartLevel;
        GameEvents.OnFinishLevel -= FinishLevel;
        GameEvents.OnStartGame -= StartGame;
        GameEvents.OnGamePause -= PauseGame;
        GameEvents.OnGameUnpause -= UnpauseGame;
        GameEvents.OnReturnToMainMenu -= ReturnToMainMenu;
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
        SceneManager.LoadScene(GameConstants.SceneNames.LoadingSceneName);
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
        SceneManager.LoadScene(GameConstants.SceneNames.MainLevelSceneName);
    }

    private void ReturnToMainMenu()
    {
        SceneManager.LoadScene(GameConstants.SceneNames.MainMenuSceneName);
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
        if (_player.GetComponent<Rigidbody2D>())
            _player.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
    }
}
