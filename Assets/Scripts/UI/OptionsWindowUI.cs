using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsWindowUI : MonoBehaviour
{
    public static event UIWindowEvent OnOptionsWindowActive;

    [SerializeField] private Button _closeButton;
    [SerializeField] private Toggle _soundsOuToggle;
    [SerializeField] private Toggle _musicOnToggle;

    private void Awake()
    {
        _closeButton.onClick.AddListener(OnCloseButtonClickHandler);
        _soundsOuToggle.onValueChanged.AddListener(OnSounsToggleValueChangeHandler);
        _musicOnToggle.onValueChanged.AddListener(OnMusicToggleValueChangetHandler);
    }

    private void Start()
    {
        SetOptionsWindow();
    }

    private void OnCloseButtonClickHandler()
    {
        gameObject.SetActive(false);
    }

    private void OnMusicToggleValueChangetHandler(bool arg0)
    {
        throw new NotImplementedException();
    }

    private void OnSounsToggleValueChangeHandler(bool arg0)
    {
        throw new NotImplementedException();
    }

    private void SetOptionsWindow()
    {
        OnOptionsWindowActive?.Invoke(this.gameObject);
    }
}
