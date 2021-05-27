using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIEquipping : NPCState
{
    private bool done;
    private List<Weapon> availableWeapons = new List<Weapon>();

    public AIEquipping(GameObject gameObject) : base(gameObject)
    {
        transitionsTo.Add(new Transition(typeof(AIChasing), () => done));
    }

    public override void AfterExecution()
    {

    }

    public override void BeforeExecution()
    {
        Debug.Log("AI Equipping Weapon");
        done = false;
        availableWeapons = weapons.CarriedWeapons;
        controller.SetDestination(transform.position);
        for (int i = 0; i < availableWeapons.Count; i++)
        {
            if (availableWeapons[i].stats.stance != WeaponStance.Shield && availableWeapons[i].stats.stance != WeaponStance.Unarmed)
            {
                weapons.EquipWeapon(i + 1); // plus 1 to get the numKey
                break;
            }
        }
        if (weapons.primarySlot.CurrentlyEquipped?.stats.stance == WeaponStance.OneHandedShield)
        {
            if (IndexOfWeaponType(WeaponStance.Shield) != -1)
            {
                Debug.Log("Equipping shield");
                weapons.EquipWeapon(IndexOfWeaponType(WeaponStance.Shield) + 1); // + 1 to get numKey
            }
        }
        done = true;
    }

    protected override void CreateTree()
    {
        
    }

    protected int IndexOfWeaponType(WeaponStance stance)
    {
        for (int i = 0; i < availableWeapons.Count; i++)
        {
            if (availableWeapons[i].stats.stance == stance)
            {
                return i;
            }
        }
        return -1;
    }

}
