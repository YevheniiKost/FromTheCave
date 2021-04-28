using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    private float _horizontalInput;
    private bool _jump;
    private float _verticalInput;
    private bool _strike;
    private bool _rangeStrike;
    private bool _block;

    private bool _readyToClear;

    public float HorizontalInput => _horizontalInput;
    public float VerticalInput  => _verticalInput;
    public bool JumpInput => _jump;
    public bool Strike  => _strike;
    public bool RangeStrike => _rangeStrike;
    public bool Block => _block;



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
            _strike = Input.GetButtonDown("Fire1");
            _rangeStrike = Input.GetKeyDown(KeyCode.E);
            _block = Input.GetButton("Fire2");
        }
    }
}
