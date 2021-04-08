using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    private float _horizontalInput;
    private bool _jump;

    private bool _readyToClear;

    public float HorizontalInput { get => _horizontalInput; private set => _ = _horizontalInput; }
    public bool JumpInput { get => _jump; private set => _ = _jump; }

    void Update()
    {
        ClearInput();

        ProcessInput();

        _horizontalInput = Mathf.Clamp(_horizontalInput, -1f, 1f);
    }

    private void FixedUpdate()
    {
        _readyToClear = true;
    }

    private void ClearInput()
    {
        if (!_readyToClear)
            return;

        _horizontalInput = 0;
        _jump = false;
    }


    private void ProcessInput()
    {
        _horizontalInput += Input.GetAxis("Horizontal");
        _jump = Input.GetButtonDown("Jump");
    }
}
