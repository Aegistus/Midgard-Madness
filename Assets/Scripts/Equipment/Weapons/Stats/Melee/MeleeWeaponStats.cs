using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewMeleeStats", menuName = "Melee Weapon Stats", order = 1)]
public class MeleeWeaponStats : WeaponStats
{
    public float knockbackForce = 5f;
}
