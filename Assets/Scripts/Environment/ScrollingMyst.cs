using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingMyst : MonoBehaviour
{
    [SerializeField] private float _scrollSpeed = -0.5f;
    private Vector2 _savedOffset;
    private Renderer _renderer;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
        _savedOffset = _renderer.material.mainTextureOffset;
    }

    private void Update()
    {
        float x = Mathf.Repeat(Time.time * _scrollSpeed, 1);
        Vector2 offset = new Vector2(x, _savedOffset.y);
        _renderer.material.mainTextureOffset = offset;
    }

    private void OnDisable()
    {
        _renderer.material.mainTextureOffset = _savedOffset;
    }
}
