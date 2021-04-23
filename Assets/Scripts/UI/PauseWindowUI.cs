using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseWindowUI : MonoBehaviour
{
    public static event UIWindowEvent OnPauseWindowActive;

    [SerializeField] private Button _continueButton;
    [SerializeField] private Button _settingsButton;
    [SerializeField] private Button _returnToMenuButton;

    private void Awake()
    {
        _continueButton.onClick.AddListener(OnContinueButtonClickHandler);
        _settingsButton.onClick.AddListener(OnSettingsButtonClickHandler);
        _returnToMenuButton.onClick.AddListener(OnReturnButtonClickHandler);
    }

    private void OnContinueButtonClickHandler()
    {
        Time.timeScale = 1;
        gameObject.SetActive(false);
    }

    private void OnSettingsButtonClickHandler()
    {
        UIManager.Instance.OptionsWindow.gameObject.SetActive(true);
    }

    private void OnReturnButtonClickHandler()
    {
        throw new NotImplementedException();
    }

    private void Start()
    {
        OnPauseWindowActive?.Invoke(gameObject);
    }
}
