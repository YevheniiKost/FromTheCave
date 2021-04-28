using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent (typeof(Button))]
public class PauseButton : MonoBehaviour
{
    private Button _pauseButton;

    private void Awake()
    {
        _pauseButton = GetComponent<Button>();
        _pauseButton.onClick.AddListener(PauseButtonClickHandler);
    }

    private void PauseButtonClickHandler()
    {
        GameEvents.RaiseOnPauseGameEvent();
    }
}
