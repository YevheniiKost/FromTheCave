using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLevel : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerController>())
        {
            GameEvents.RaiseOnFinishLevelEvent();
            AudioManager.Instance.PlaySFX(SoundsFx.WinGame);
        }
    }
}
