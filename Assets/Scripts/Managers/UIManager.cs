using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void UIWindowEvent(GameObject window);
public class UIManager : MonoBehaviour
{
    public MainWindowUI MainWindow;
    public OptionsWindowUI OptionsWindow;
    public YouFailderWindowUI YouFailedWindow;
    public MessageWindowUI MessageWindow;
    public GameObject YouWinWindow;

    public static UIManager Instance;

    private void Awake()
    {
        Instance = this;
        SubscribeToEvents();
    }
    private void OnDestroy()
    {
        UnSubscribeToEvents();
    }

    private void Start()
    {
        if(YouFailedWindow != null)
        YouFailedWindow.gameObject.SetActive(false);

        if(YouWinWindow != null)
        YouWinWindow.SetActive(false);
    }

    private void SubscribeToEvents()
    {
        EventAggregator.OnPlayerDeath += ProcessPlayerDeath;
        EventAggregator.OnLoadGame += LoadGame;
        EventAggregator.OnFinishLevel += FinishLevel;

        MainWindowUI.OnMainWindowActive += GetMainWindow;
        OptionsWindowUI.OnOptionsWindowActive += GetOptionsWindow;
        MessageWindowUI.OnMessageWindowActive += GetMessageWindow;
    }

    private void UnSubscribeToEvents()
    {
        EventAggregator.OnPlayerDeath -= ProcessPlayerDeath;
        EventAggregator.OnLoadGame -= LoadGame;
        EventAggregator.OnFinishLevel -= FinishLevel;

        MainWindowUI.OnMainWindowActive -= GetMainWindow;
        OptionsWindowUI.OnOptionsWindowActive -= GetOptionsWindow;
        MessageWindowUI.OnMessageWindowActive -= GetMessageWindow;
    }

    private void FinishLevel()
    {
        YouWinWindow.SetActive(true);
    }

    private void LoadGame()
    {
        YouFailedWindow.gameObject.SetActive(false);
    }

    private void ProcessPlayerDeath()
    {
        YouFailedWindow.gameObject.SetActive(true);
    }

    #region GetWindows
    private void GetMainWindow(GameObject window)
    {
        if (MainWindow == null)
        {
            MainWindow = window.GetComponent<MainWindowUI>();
        }
    }

    private void GetOptionsWindow(GameObject window)
    {
        if(OptionsWindow == null)
        {
            OptionsWindow = window.GetComponent<OptionsWindowUI>();
            OptionsWindow.gameObject.SetActive(false);
        }
    }

    private void GetMessageWindow(GameObject window)
    {
        if(MessageWindow == null)
        {
            MessageWindow = window.GetComponent<MessageWindowUI>();
            MessageWindow.gameObject.SetActive(false);
        }
    }

    #endregion
}
