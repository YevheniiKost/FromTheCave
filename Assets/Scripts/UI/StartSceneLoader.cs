using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartSceneLoader : MonoBehaviour
{
    [SerializeField] private float _startSceneDealy = 5f;

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(_startSceneDealy);
        SceneManager.LoadSceneAsync(GameConstants.SceneNames.MainMenuSceneName);
    }
}
