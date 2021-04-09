using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField] private int _noWeaponDamage = 10;
    [SerializeField] private int _swordDamage = 50;
    [SerializeField] private int _bowDamage = 30;


    public bool IsHasSword;
    public bool IsHasBow;


}
