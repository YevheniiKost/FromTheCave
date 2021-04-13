using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class StalactiteTrap : MonoBehaviour
{
    [SerializeField] private Transform _stalactite;

    [SerializeField] private float _shakeDuration;
    [SerializeField] private float _shakeStrenght;
    [SerializeField] private int _shakeVibratio;
    [SerializeField] private float _shakeRandomness;
    [SerializeField] private float _fallingSpeed;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerController>())
        {
            StartCoroutine(StalactiteCoroutine());

            Destroy(this.gameObject, 5f);
        }
    }

    private IEnumerator StalactiteCoroutine()
    {
        if(_stalactite != null)
        _stalactite.DOShakePosition(_shakeDuration, _shakeStrenght, _shakeVibratio, _shakeRandomness);

        yield return new WaitForSeconds(_shakeDuration);

        while (_stalactite != null)
        {
            _stalactite.position += Vector3.down * _fallingSpeed * Time.deltaTime;
            yield return null;
        }
    }
}
