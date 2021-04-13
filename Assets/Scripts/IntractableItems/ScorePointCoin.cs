using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScorePointCoin : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent(out PlayerController player))
        {
            player.GetScore();
            Destroy(this.gameObject);
        }
    }
}
