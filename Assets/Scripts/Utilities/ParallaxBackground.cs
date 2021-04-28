using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    [SerializeField] private ParallaxCamera _parallaxCamera;

    private List<ParallaxLayer> _parallaxLayers = new List<ParallaxLayer>();

    private void Start()
    {
        if (_parallaxCamera == null)
            _parallaxCamera = Camera.main.GetComponent<ParallaxCamera>();
        if (_parallaxCamera != null)
            _parallaxCamera.onCameraTranslate += Move;
        SetLayers();
    }

    private void SetLayers()
    {
        _parallaxLayers.Clear();
        for (int i = 0; i < transform.childCount; i++)
        {
            ParallaxLayer layer = transform.GetChild(i).GetComponent<ParallaxLayer>();

            if (layer != null)
            {
                layer.name = "Layer-" + i;
                _parallaxLayers.Add(layer);
            }
        }
    }

    private void Move(float delta)
    {
        foreach (ParallaxLayer layer in _parallaxLayers)
        {
            layer.Move(delta);
        }
    }
}
