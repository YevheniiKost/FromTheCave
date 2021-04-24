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
    [SerializeField] private Slider _soundsVolumeSlider;
    [SerializeField] private Slider _musicVolumeSlider;

    private void Awake()
    {
        _closeButton.onClick.AddListener(OnCloseButtonClickHandler);
        _soundsOuToggle.onValueChanged.AddListener(OnSounsToggleValueChangeHandler);
        _musicOnToggle.onValueChanged.AddListener(OnMusicToggleValueChangeHandler);
        _soundsVolumeSlider.onValueChanged.AddListener(OnSounsVolumeSliderValueChangeHandler);
        _musicVolumeSlider.onValueChanged.AddListener(OnMusicVolumeSliderValueChangeHandler);
    }

    private void Start()
    {
        SetOptionsWindow();
        _soundsVolumeSlider.value = AudioManager.Instance.SoundsFXVolume;
        _musicVolumeSlider.value = AudioManager.Instance.MusicVolume;
        _soundsOuToggle.isOn = !AudioManager.Instance.IsSoundsOff;
        _musicOnToggle.isOn = !AudioManager.Instance.IsMusicOff;
    }

    private void OnCloseButtonClickHandler()
    {
        gameObject.SetActive(false);
    }

    private void OnMusicToggleValueChangeHandler(bool isOn)
    {
        AudioManager.Instance.SetMusic(!isOn);
    }

    private void OnSounsToggleValueChangeHandler(bool isOn)
    {
        AudioManager.Instance.SetSoundFX(!isOn);
    }
    private void OnMusicVolumeSliderValueChangeHandler(float value)
    {
        AudioManager.Instance.SetMusicVolume(value);
    }

    private void OnSounsVolumeSliderValueChangeHandler(float value)
    {
        AudioManager.Instance.SetFXVolume(value);
    }

    private void SetOptionsWindow()
    {
        OnOptionsWindowActive?.Invoke(this.gameObject);
    }
}
