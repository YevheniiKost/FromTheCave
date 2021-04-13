using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileAxe : MonoBehaviour
{
    [SerializeField] private float _rotationSpeed = 1f;
    [SerializeField] private float _movingSpeed = 2f;
    [SerializeField] private float _destroyTimer;

    private int _currentDamage;
    private float _direction;
    
    public void GetPlayerData(int damage, float direction)
    {
        _currentDamage = damage;
        _direction = direction;
    }

    private void Update()
    {
        transform.position += Vector3.right * _movingSpeed * _direction * Time.deltaTime;
    }

    private void OnEnable()
    {
       StartCoroutine(StartRotation());
        Destroy(this.gameObject, _destroyTimer);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent(out EnemyHealth enemy))
        {
            enemy.GetHit(_currentDamage);
            Destroy(this.gameObject);
        }
    }

    private IEnumerator StartRotation()
    {
        int number = 0;
        while(number < 360)
        {
            transform.localRotation = Quaternion.Euler(0, 0, -number*10 * _direction);
            yield return new WaitForSeconds(_rotationSpeed);
            number++;
        }
        StartCoroutine(StartRotation());
        yield return null;
    }
}
