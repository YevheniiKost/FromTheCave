using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpSword : PickupableItem
{ 
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent(out PlayerCombat player))
        {
            player.GetSword();
            AudioManager.Instance.PlaySFX(SoundsFx.GetWeapon);
            DisableItem(false);
            isActive = false;
        }
    }
}
