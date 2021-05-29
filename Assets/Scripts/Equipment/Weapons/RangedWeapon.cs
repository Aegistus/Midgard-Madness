using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedWeapon : Weapon
{
    public Transform projectileSpawnPoint;

    private RangedWeaponStats rangedStats;

    public int QuiverCapacity = 35;

    private int Quiver;

    private int arrow;

    
    private void Awake()
    {
        rangedStats = (RangedWeaponStats)stats;

        Quiver = QuiverCapacity;
    }

    public void Aim()
    {

    }

    public void AddArrows(int arrow)
    {
        //Pickuo arrows or bundle

        //Assign int to pickup

        //Another way to add arrows?
        this.arrow = arrow;
        
        Quiver += arrow;
        
        if ( Quiver >= QuiverCapacity)
        {
            Quiver = QuiverCapacity;
        }
        else if(Quiver < QuiverCapacity)
        {
            
        }
        
        

    }

    public void RangedAttack(Ray aim)
    {

        if (Quiver <= 0)
        {
            Quiver = 0; 
            
        }
        else if (Quiver <= QuiverCapacity)
        {
            Transform projectileTransform = Instantiate(rangedStats.projectile, projectileSpawnPoint.position, projectileSpawnPoint.rotation).transform;
            projectileTransform.LookAt(aim.GetPoint(10));
            Quiver--;
            
        }

        
    }
}
