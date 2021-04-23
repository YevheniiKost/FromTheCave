using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyToDoor : MonoBehaviour
{
    private bool _isActivated = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerCombat>() && !_isActivated)
        {
            _isActivated = true;
            GetComponentInParent<LockedDoor>().OpenDoor();
            GetComponent<CapsuleCollider2D>().enabled = false;
            GetComponent<SpriteRenderer>().enabled = false;
        }
    }
}
