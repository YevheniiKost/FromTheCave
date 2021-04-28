﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShowTipObject : MonoBehaviour
{
    [TextArea][SerializeField]
    private string _tipText;

    private bool _wasShow = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerController>() && !_wasShow)
        {
            TipsSystem.Instance.ShowTip(_tipText);
            _wasShow = true;
        }
    }

}
