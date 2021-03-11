using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class AgentWeapons : MonoBehaviour
{
    public WeaponSlot primarySlot;
    public WeaponSlot secondarySlot;
    private List<Weapon> carriedWeapons;

    public WeaponStance CurrentStance { get; private set; }

    private Animator anim;

    private void Start()
    {
        anim = GetComponentInChildren<Animator>();
        carriedWeapons = GetComponentsInChildren<Weapon>().ToList();
        foreach (var weapon in carriedWeapons)
        {
            if (weapon.stats?.stance == WeaponStance.Unarmed)
            {
                EquipWeapon(weapon);
            }
            else
            {
                weapon.gameObject.SetActive(false);
            }
        }
    }

    public void EquipWeapon(Weapon toEquip)
    {
        switch (toEquip.stats.stance)
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
                if (primarySlot.CurrentlyEquipped?.stats.stance == WeaponStance.TwoHanded)
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
            CurrentStance = primarySlot.CurrentlyEquipped.stats.stance;
            animController = primarySlot.CurrentlyEquipped.stats.weaponAnimationSet;
        }
        else if (secondarySlot.CurrentlyEquipped != null)
        {
            CurrentStance = secondarySlot.CurrentlyEquipped.stats.stance;
            animController = secondarySlot.CurrentlyEquipped.stats.weaponAnimationSet;
        }
        // set animation controller to the weapon's controller
        if (animController != null)
        {
            anim.runtimeAnimatorController = animController;
        }
    }
}
