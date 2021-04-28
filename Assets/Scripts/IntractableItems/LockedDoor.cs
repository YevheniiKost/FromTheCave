using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[RequireComponent (typeof(BoxCollider2D)), RequireComponent (typeof(SpriteRenderer))]
public class LockedDoor : MonoBehaviour
{
    [SerializeField] private float _openMovementDistance = 2f;
    [SerializeField] private float _openDuration = 1f;
    public bool IsOpened => _isOpened;

    private bool _isOpened;

    private void Awake()
    {
        _isOpened = false;
    }
    public void OpenDoor()
    {
        if (!_isOpened)
        {
            _isOpened = true;
        }
    }

    public void PlayerOpenDoorAnimation()
    {
        gameObject.transform.DOLocalMoveX(_openMovementDistance, _openDuration).SetEase(Ease.Flash);
        AudioManager.Instance.PlaySFX(SoundsFx.OpenDoor);
    }
}
