using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthbar : MonoBehaviour
{
    [SerializeField] private Image _foregroundImage;
    [SerializeField] private float _updateSpeedSeconds = .5f;

    private void Awake()
    {
        GetComponentInParent<EnemyHealth>().OnHealthPctChanged += HandleHealthChanged;
    }


    private void HandleHealthChanged(float pct)
    {
        if(gameObject.activeSelf)
        StartCoroutine(ChangeHealth(pct));
    }

    private IEnumerator ChangeHealth(float pct)
    {
        float preChangerPct = _foregroundImage.fillAmount;
        float elapsed = 0f;

        while(elapsed < _updateSpeedSeconds)
        {
            elapsed += Time.deltaTime;
            _foregroundImage.fillAmount = Mathf.Lerp(preChangerPct, pct, elapsed / _updateSpeedSeconds);
            yield return null;
        }

        _foregroundImage.fillAmount = pct;

        if(pct <= 0)
        {
            this.gameObject.SetActive(false);
        }
        yield return null;

    }
}
