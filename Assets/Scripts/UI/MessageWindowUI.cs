using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MessageWindowUI : MonoBehaviour
{
    public static event UIWindowEvent OnMessageWindowActive;

    [SerializeField] private TextMeshProUGUI _messageText;
    [SerializeField] private Button _yesButton;
    [SerializeField] private Button _noButton;

    private Action _onYesButtonCallback;

    public void CreateMessageWindow(string messageText, Action yesButtonCallback)
    {
        gameObject.SetActive(true);
        if(messageText != " ")
        {
            _messageText.text = messageText;
        }
        else
        {
            _messageText.text = $"Are you sure?";
        }

        _onYesButtonCallback = yesButtonCallback;
    }

    private void Awake()
    {
        _yesButton.onClick.AddListener(OnYesButtonClickHandler);
        _noButton.onClick.AddListener(OnNoButtonClickHandler);
    }

    private void Start()
    {
        OnMessageWindowActive?.Invoke(this.gameObject);
    }

    private void OnNoButtonClickHandler()
    {
        gameObject.SetActive(false);
    }

    private void OnYesButtonClickHandler()
    {
        _onYesButtonCallback.Invoke();
    }
}
