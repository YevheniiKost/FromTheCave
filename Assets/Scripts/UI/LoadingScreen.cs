using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingScreen : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _loadingText;

    private void Start()
    {
        StartCoroutine(LoadingDotsCycle());
        StartCoroutine(LoadAsyncOperation());
    }
   
    private IEnumerator LoadAsyncOperation()
    {
        AsyncOperation gameLevel = SceneManager.LoadSceneAsync(1);

        while(gameLevel.progress < 1)
        {
            yield return null;
        }
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

            yield return new WaitForEndOfFrame();
        }
    }
}
