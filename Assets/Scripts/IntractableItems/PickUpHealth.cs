using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpHealth : PickupableItem
{
    [SerializeField] private int _healthAmount = 1;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out PlayerHealth player))
        {
            player.ModifyHealth(_healthAmount);
            AudioManager.Instance.PlaySFX(SoundsFx.HealthPotion);
            DisableItem(false);
            isActive = false;
        }
    }
}
