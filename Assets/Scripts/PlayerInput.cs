using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    private float _horizontalInput;
    private bool _jump;
    private float _verticalInput;

    private bool _readyToClear;

    public float HorizontalInput { get => _horizontalInput; private set => _ = _horizontalInput; }
    public float VerticalInput { get => _verticalInput; private set => _ = _verticalInput; }
    public bool JumpInput { get => _jump; private set => _ = _jump; }


    void Update()
    {
        ClearInput();

        ProcessInput();

        _horizontalInput = Mathf.Clamp(_horizontalInput, -1f, 1f);
        _verticalInput = Mathf.Clamp(_verticalInput, -1f, 1f);
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
        _verticalInput = 0;
        _jump = false;
    }


    private void ProcessInput()
    {
        if (!GetComponent<PlayerHealth>().IsCharacterDead)
        {
            _horizontalInput += Input.GetAxis("Horizontal");
            _verticalInput += Input.GetAxis("Vertical");
            _jump = Input.GetButtonDown("Jump");
        }
    }
}
