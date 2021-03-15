using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponStats : ScriptableObject
{
    public WeaponStance stance;
    public float damage;
    public float attackSpeed;
    public float movementMultiplier = 1f;
    public AnimatorOverrideController weaponAnimationSet;
}
