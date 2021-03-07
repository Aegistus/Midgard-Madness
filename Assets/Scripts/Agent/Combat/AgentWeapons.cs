using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AgentWeapons : MonoBehaviour
{
    public WeaponSlot primarySlot;
    public WeaponSlot secondarySlot;
    public List<Weapon> carriedWeapons;

    public WeaponStance CurrentStance { get; private set; }

    private Animator anim;

    private void Start()
    {
        anim = GetComponentInChildren<Animator>();
        foreach (var weapon in carriedWeapons)
        {
            if (weapon.stance == WeaponStance.Unarmed)
            {
                EquipWeapon(weapon);
            }
        }
    }

    public void EquipWeapon(Weapon toEquip)
    {
        switch (toEquip.stance)
        {
            case WeaponStance.OneHandedShield:
                primarySlot.Equip(toEquip);
                break;
            case WeaponStance.TwoHanded:
                primarySlot.Equip(toEquip);
                secondarySlot.UnEquip();
                break;
            case WeaponStance.Bow:
                secondarySlot.Equip(toEquip);
                primarySlot.UnEquip();
                break;
            case WeaponStance.Shield:
                secondarySlot.UnEquip();
                secondarySlot.Equip(toEquip);
                if (primarySlot.CurrentlyEquipped?.stance == WeaponStance.TwoHanded)
                {
                    primarySlot.UnEquip();
                }
                break;
            default:
                primarySlot.UnEquip();
                secondarySlot.UnEquip();
                break;
        }
        UpdateWeaponAnimation();
    }

    public void EquipWeapon(int numKey)
    {
        if (numKey - 1 < carriedWeapons.Count && numKey - 1 >= 0)
        {
            Weapon toEquip = carriedWeapons[numKey - 1];
            EquipWeapon(toEquip);
        }
    }

    public void UpdateWeaponAnimation()
    {
        AnimatorOverrideController animController = null;
        if (primarySlot.CurrentlyEquipped != null)
        {
            CurrentStance = primarySlot.CurrentlyEquipped.stance;
            animController = primarySlot.CurrentlyEquipped.animationController;
        }
        else if (secondarySlot.CurrentlyEquipped != null)
        {
            CurrentStance = secondarySlot.CurrentlyEquipped.stance;
            animController = secondarySlot.CurrentlyEquipped.animationController;
        }
        // set animation controller to the weapon's controller
        if (animController != null)
        {
            anim.runtimeAnimatorController = animController;
        }
    }
}
