using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Scorebar : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _scoreText;

    private void Awake()
    {
        SubscribeToEvents();
    }

    private void Start()
    {
        _scoreText.text = $"0";
    }

    private void OnDestroy()
    {
        UnsubscribeToEvents();
    }

    private void SubscribeToEvents()
    {
        GameEvents.OnChangeScore += UpdateScoreText;
    }

    private void UnsubscribeToEvents()
    {
        GameEvents.OnChangeScore -= UpdateScoreText;
    }

    private void UpdateScoreText(int score)
    {
        _scoreText.text = $"{score}";
    }
}
