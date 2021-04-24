using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TipsSystem : MonoBehaviour
{
    [SerializeField] private float _tipDuration;

    public TipWindowUI TipWindow;

    public static TipsSystem Instance;

    private Coroutine _currentTip;

    private void Awake()
    {
        Instance = this;
        TipWindow.gameObject.SetActive(false);
    }
    public void ShowTip(string tipText)
    {
        if(_currentTip != null)
        StopCoroutine(_currentTip);

        _currentTip = StartCoroutine(ShowTipWindow(tipText));
    }

    private IEnumerator ShowTipWindow(string tipText)
    {
        TipWindow.SetTipWindow(tipText);
        TipWindow.gameObject.SetActive(true);

        yield return new WaitForSeconds(_tipDuration);

        TipWindow.gameObject.SetActive(false);

        _currentTip = null;
    }
}
