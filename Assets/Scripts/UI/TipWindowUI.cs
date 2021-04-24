using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TipWindowUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _tipText;

    public void SetTipWindow( string tipText)
    {
        _tipText.text = tipText;
    }
}
