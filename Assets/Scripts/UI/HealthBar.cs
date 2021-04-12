using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{

    private List<Transform> _heartsList = new List<Transform>();

    private void Awake()
    {
        FindObjectOfType<PlayerHealth>().OnHealthChanged += ChangeHealth;

        FillHeartsArray();
    }

    private void FillHeartsArray()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            _heartsList.Add(transform.GetChild(i));
        }
    }

    private void ChangeHealth(int currentHealth)
    {
        foreach (var heart in _heartsList)
        {
            heart.GetComponent<Image>().color = Color.black;
        }
        for (int i = 0; i < currentHealth; i++)
        {
            _heartsList[i].GetComponent<Image>().color = Color.white;
        }
    }
}
