using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

[RequireComponent(typeof(CapsuleCollider2D)), RequireComponent(typeof(SpriteRenderer))]
public class KeyToDoor : MonoBehaviour
{
    private bool _isActivated = false;
    [SerializeField] private LockedDoor _door;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerCombat>() && !_isActivated)
        {
            _isActivated = true;
            _door.OpenDoor();

            AudioManager.Instance.PlaySFX(SoundsFx.GetKey);

            if (GetComponentInChildren<Light2D>())
                GetComponentInChildren<Light2D>().enabled = false;

            GetComponent<CapsuleCollider2D>().enabled = false;
            GetComponent<SpriteRenderer>().enabled = false;
        }
    }
}
