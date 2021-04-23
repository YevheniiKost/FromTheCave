using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LoadingScreen : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _loadingText;

    private void Start()
    {
        StartCoroutine(LoadingDotsCycle());
    }

    private IEnumerator LoadingDotsCycle()
    {
        string loading = $"Loading";
        while (true)
        {
            _loadingText.text = $"{loading}.";
            yield return new WaitForSeconds(.5f);
            _loadingText.text = $"{loading}..";
            yield return new WaitForSeconds(.5f);
            _loadingText.text = $"{loading}...";
            yield return new WaitForSeconds(.5f);

        }
    }
}
