using UnityEngine;
using UnityEngine.UI;

public class YouFailderWindowUI : MonoBehaviour
{
    [SerializeField] private Button _tryAgainFromCheckpointButton;
    [SerializeField] private Button _restartLevelButton;

    private void Awake()
    {
        _tryAgainFromCheckpointButton.onClick.AddListener(OnTryAgainButtonClickHandler);
        _restartLevelButton.onClick.AddListener(OnRestartLevelButtonClickHandler);
    }

    private void OnRestartLevelButtonClickHandler()
    {
        EventAggregator.RaiseOnRestartLevelEvent();
    }

    private void OnTryAgainButtonClickHandler()
    {
        EventAggregator.RaiseOnLoadGameEvent();
    }
}
