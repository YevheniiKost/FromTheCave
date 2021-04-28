using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stalactite : MonoBehaviour
{
    [SerializeField] private int _damage = 1;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent(out PlayerHealth player))
        {
            player.ModifyHealth(-_damage);
        }

        Destroy(this.gameObject);
    }
}
