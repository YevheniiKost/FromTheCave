using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorPlayerDetector : MonoBehaviour
{
    [SerializeField] private LockedDoor _door;
    [SerializeField] private ShowTipObject _tip;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerController>())
        {
            if (_door.IsOpened)
            {
                _tip.gameObject.SetActive(false);
                _door.PlayerOpenDoorAnimation();
            }
        }
    }
}
