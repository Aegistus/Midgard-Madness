using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedWeapon : Weapon
{
    public Transform projectileSpawnPoint;

    private RangedWeaponStats rangedStats;

    private void Awake()
    {
        rangedStats = (RangedWeaponStats)stats;
    }

    public void Aim()
    {

    }

    public void RangedAttack()
    {
        GameObject projectile = Instantiate(rangedStats.projectile, projectileSpawnPoint.position, projectileSpawnPoint.rotation);
    }
}
