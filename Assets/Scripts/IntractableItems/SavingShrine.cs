using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavingShrine : MonoBehaviour
{
    [SerializeField] private ParticleSystem _saveParticles;
    [SerializeField] private float _reloadTime = 10f;

    private bool _readyToSave = true;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerController>())
        {
            if(_readyToSave)
            SaveGame();
        }
    }

    private void SaveGame()
    {
        StartCoroutine(ReloadShrineTimer());
        _saveParticles.Play();
        EventAggregator.RaiseOnSaveGameEvent();
    }

    private IEnumerator ReloadShrineTimer()
    {
        _readyToSave = false;
        yield return new WaitForSeconds(_reloadTime);
        _readyToSave = true;
        yield return null;
    }
}
