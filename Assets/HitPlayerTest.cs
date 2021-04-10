using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitPlayerTest : MonoBehaviour
{
    PlayerHealth player;

    private void Awake()
    {
        player = FindObjectOfType<PlayerHealth>();
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            player.ModifyHealth(-1);
        }   
    }
}
