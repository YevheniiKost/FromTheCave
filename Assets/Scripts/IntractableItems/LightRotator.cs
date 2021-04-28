using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class LightRotator : MonoBehaviour
{
    [SerializeField] private float _rotationSpeed = 2f;

    private Vector3 _rot = new Vector3(0, 0, 1);
    void Update()
    {
        _rot.z += _rotationSpeed * Time.deltaTime;
        if (_rot.z > 360)
            _rot.z = 0;
        transform.localRotation = Quaternion.Euler(_rot);
    }

}
    
