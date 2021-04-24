using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class YouWinWindowUI : MonoBehaviour
{
    public static event UIWindowEvent OnYouWinWindowActive;

    [SerializeField] private TextMeshProUGUI _coinText;
    [SerializeField] private TextMeshProUGUI _timeText;

    public void SetWinWindow(int coinsCount, float levelCompleteTime)
    {
        _coinText.text = $"{coinsCount}";
        TimeSpan time = TimeSpan.FromSeconds(levelCompleteTime);
        //_timeText.text = $"{time.Minutes}:{time.Seconds}";
        _timeText.text = time.ToString(@"mm\:ss");
    }

    private void Start()
    {
        OnYouWinWindowActive?.Invoke(gameObject);
    }
}
