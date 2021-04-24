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
    public YouWinWindowUI YouWinWindow;
    public PauseWindowUI PauseWindow;

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
    }

    #region HandleEvents
    private void SubscribeToEvents()
    {
        EventAggregator.OnPlayerDeath += ProcessPlayerDeath;
        EventAggregator.OnLoadGame += LoadGame;
        EventAggregator.OnFinishLevel += FinishLevel;
        EventAggregator.OnGamePause += PauseGame;
        EventAggregator.OnGameUnpause += UnpauseGame;

        MainWindowUI.OnMainWindowActive += GetMainWindow;
        OptionsWindowUI.OnOptionsWindowActive += GetOptionsWindow;
        MessageWindowUI.OnMessageWindowActive += GetMessageWindow;
        PauseWindowUI.OnPauseWindowActive += GetPauseWindow;
        YouWinWindowUI.OnYouWinWindowActive += GetWinWindow;
        YouFailderWindowUI.OnYouFailedWindowActive += GetYouFailedWindow;
    }

    private void UnSubscribeToEvents()
    {
        EventAggregator.OnPlayerDeath -= ProcessPlayerDeath;
        EventAggregator.OnLoadGame -= LoadGame;
        EventAggregator.OnFinishLevel -= FinishLevel;
        EventAggregator.OnGamePause -= PauseGame;
        EventAggregator.OnGameUnpause -= UnpauseGame;

        MainWindowUI.OnMainWindowActive -= GetMainWindow;
        OptionsWindowUI.OnOptionsWindowActive -= GetOptionsWindow;
        MessageWindowUI.OnMessageWindowActive -= GetMessageWindow;
        PauseWindowUI.OnPauseWindowActive -= GetPauseWindow;
        YouWinWindowUI.OnYouWinWindowActive -= GetWinWindow;
        YouFailderWindowUI.OnYouFailedWindowActive -= GetYouFailedWindow;
    }

    #endregion

    private void FinishLevel()
    {
        YouWinWindow.gameObject.SetActive(true);
    }

    private void LoadGame()
    {
        YouFailedWindow.gameObject.SetActive(false);
    }

    private void PauseGame()
    {
        PauseWindow.gameObject.SetActive(true);
    }

    private void UnpauseGame()
    {
        PauseWindow.gameObject.SetActive(false);
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

    private void GetPauseWindow(GameObject window)
    {
        if (PauseWindow == null)
        {
            PauseWindow = window.GetComponent<PauseWindowUI>();
            PauseWindow.gameObject.SetActive(false);
        }
    }

    private void GetWinWindow(GameObject window)
    {
        if (YouWinWindow == null)
        {
            YouWinWindow = window.GetComponent<YouWinWindowUI>();
            YouWinWindow.gameObject.SetActive(false);
        }
    }

    private void GetYouFailedWindow(GameObject window)
    {
        if (YouFailedWindow == null)
        {
            YouFailedWindow = window.GetComponent<YouFailderWindowUI>();
            YouFailedWindow.gameObject.SetActive(false);
        }
    }

    #endregion
}
