using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpAxe : PickupableItem
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out PlayerCombat player))
        {
            player.GetAxe();
            AudioManager.Instance.PlaySFX(SoundsFx.GetWeapon);
            DisableItem(false);
            isActive = false;
        }
    }
}
