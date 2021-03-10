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

    public void RangedAttack(Ray aim)
    {
        Transform projectileTransform = Instantiate(rangedStats.projectile, projectileSpawnPoint.position, projectileSpawnPoint.rotation).transform;
        projectileTransform.LookAt(aim.GetPoint(10));
    }
}
