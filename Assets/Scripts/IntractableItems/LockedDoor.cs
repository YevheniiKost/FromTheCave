using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockedDoor : MonoBehaviour
{
    private bool _isOpened = false;
    public void OpenDoor()
    {
        if (!_isOpened)
        {
            GetComponent<BoxCollider2D>().enabled = false;
            GetComponent<SpriteRenderer>().enabled = false;
            _isOpened = true;
        }
    }

    private void CloseDoore()
    {
        if (_isOpened)
        {
            GetComponent<BoxCollider2D>().enabled = false;
            GetComponent<SpriteRenderer>().enabled = false;
            _isOpened = false;
        }
    }
    
}
