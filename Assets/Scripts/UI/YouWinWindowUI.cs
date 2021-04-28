using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class YouWinWindowUI : MonoBehaviour
{
    public static event UIWindowEvent OnYouWinWindowActive;

    [SerializeField] private TextMeshProUGUI _coinText;
    [SerializeField] private TextMeshProUGUI _timeText;
    [SerializeField] private Button _returnButton; 

    public void SetWinWindow(int coinsCount, float levelCompleteTime)
    {
        _coinText.text = $"{coinsCount}";
        TimeSpan time = TimeSpan.FromSeconds(levelCompleteTime);
        _timeText.text = time.ToString(@"mm\:ss");
    }

    private void Awake()
    {
        _returnButton.onClick.AddListener(OnReturnButtonClickHandler);
    }

    private void Start()
    {
        OnYouWinWindowActive?.Invoke(gameObject);
    }

    private void OnReturnButtonClickHandler()
    {
        GameEvents.RaiseOnReturnToMainMenu();
    }
}
