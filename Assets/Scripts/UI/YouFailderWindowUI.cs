using System;
using UnityEngine;
using UnityEngine.UI;

public class YouFailderWindowUI : MonoBehaviour
{
    public static event UIWindowEvent OnYouFailedWindowActive;

    [SerializeField] private Button _tryAgainFromCheckpointButton;
    [SerializeField] private Button _restartLevelButton;
    [SerializeField] private Button _returnToMainMenuButton;

    private void Awake()
    {
        _tryAgainFromCheckpointButton.onClick.AddListener(OnTryAgainButtonClickHandler);
        _restartLevelButton.onClick.AddListener(OnRestartLevelButtonClickHandler);
        _returnToMainMenuButton.onClick.AddListener(OnReturnToMainMenuButtonClickHandler);
    }

    private void Start()
    {
        OnYouFailedWindowActive?.Invoke(gameObject);
    }

    private void OnReturnToMainMenuButtonClickHandler()
    {
        UIManager.Instance.MessageWindow.CreateMessageWindow(" ", ReturnToMainMenu);
    }

    private void OnRestartLevelButtonClickHandler()
    {
        UIManager.Instance.MessageWindow.CreateMessageWindow("Restart level?", RestartLevel);
    }

    private void OnTryAgainButtonClickHandler()
    {
        GameEvents.RaiseOnLoadGameEvent();
    }

    private void RestartLevel()
    {
        GameEvents.RaiseOnRestartLevelEvent();
    }

    private void ReturnToMainMenu()
    {
        GameEvents.RaiseOnReturnToMainMenu();
    }
}
