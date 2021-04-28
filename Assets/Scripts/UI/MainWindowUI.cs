using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class MainWindowUI : MonoBehaviour
{
    public static event UIWindowEvent OnMainWindowActive;

    [SerializeField] private Button _startButton;
    [SerializeField] private Button _optionsButton;
    [SerializeField] private Button _exitGameButton;

    private void Awake()
    {
        _startButton.onClick.AddListener(OnStartButtonClickHandler);
        _optionsButton.onClick.AddListener(OnOptionsButtonClickHandler);
        _exitGameButton.onClick.AddListener(OnExitButtonClickHandler);
    }

    private void Start()
    {
        OnMainWindowActive?.Invoke(this.gameObject);
    }

    private void OnExitButtonClickHandler()
    {
        UIManager.Instance.MessageWindow.CreateMessageWindow("Exit game?", CloseApplication);
    }

    private void OnOptionsButtonClickHandler()
    {
        UIManager.Instance.OptionsWindow.gameObject.SetActive(true);
    }

    private void OnStartButtonClickHandler()
    {
        GameEvents.RaiseOnGameStartEvent();
    }

    private void CloseApplication()
    {
        Application.Quit();
    }
}
