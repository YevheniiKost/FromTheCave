using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class StartSceneLoader : MonoBehaviour
{
    
    [SerializeField] private TextMeshProUGUI _pressAnyKeyText;
    [SerializeField] private float _shakeDuration = 10f;
    [SerializeField]private float _shakeStrenght = 1f;
    [SerializeField]private int _shakeVibratio = 10;
    [SerializeField] private float _shakeRandomness = 90f;

    Tweener _tween;

    private void Start()
    {
       _tween = _pressAnyKeyText?.transform.DOShakeScale(_shakeDuration, _shakeStrenght, _shakeVibratio, _shakeRandomness).SetLoops(-1, LoopType.Restart).SetEase(Ease.Linear);
    }

    private void Update()
    {
        if(Input.anyKey)
            SceneManager.LoadSceneAsync(GameConstants.SceneNames.MainMenuSceneName);
    }

    private void OnDestroy()
    {
        _tween.Kill();
    }
}
