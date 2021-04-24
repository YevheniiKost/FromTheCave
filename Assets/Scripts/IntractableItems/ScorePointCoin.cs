using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScorePointCoin : PickupableItem
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent(out PlayerController player))
        {
            player.GetScore();
            AudioManager.Instance.PlaySFX(SoundsFx.CollectCoin);
            DisableItem(false);
            isActive = false;
        }
    }

    
}
