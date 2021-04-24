using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationEventsHandler : MonoBehaviour
{
    private PlayerCombat _player;

    private void Awake()
    {
        _player = transform.parent.GetComponent<PlayerCombat>();
    }

    public void HandleAttack()
    {
        _player.Attack();
    }

    public void PlayFootstepSound()
    {
        AudioManager.Instance.PlaySFX(SoundsFx.Footstep);
    }
}
