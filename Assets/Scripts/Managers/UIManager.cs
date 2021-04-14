using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private YouFailderWindowUI _youFailedWindow;

    private void Awake()
    {
        SubscribeToEvents();
    }
    private void OnDestroy()
    {
        UnSubscribeToEvents();
    }

    private void Start()
    {
        _youFailedWindow.gameObject.SetActive(false);    
    }

    private void SubscribeToEvents()
    {
        EventAggregator.OnPlayerDeath += ProcessPlayerDeath;
        EventAggregator.OnLoadGame += LoadGame;
    }

    private void UnSubscribeToEvents()
    {
        EventAggregator.OnPlayerDeath -= ProcessPlayerDeath;
        EventAggregator.OnLoadGame -= LoadGame;
    }

    private void LoadGame()
    {
        _youFailedWindow.gameObject.SetActive(false);
    }

    private void ProcessPlayerDeath()
    {
        _youFailedWindow.gameObject.SetActive(true);
    }
}
