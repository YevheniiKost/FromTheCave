using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class GlobalLightIntencityChanger : MonoBehaviour
{
    [SerializeField] private Light2D _light;
    [SerializeField] private float _intesity = 1.5f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerController>())
        {
            _light.intensity = _intesity;
        }
    }
}
