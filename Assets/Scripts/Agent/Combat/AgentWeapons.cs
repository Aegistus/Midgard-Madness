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
    private Dictionary<WeaponStance, int> equipmentStanceLayers = new Dictionary<WeaponStance, int>();

    private void Start()
    {
        anim = GetComponentInChildren<Animator>();
        equipmentStanceLayers.Add(WeaponStance.Unarmed, anim.GetLayerIndex("Unarmed"));
        equipmentStanceLayers.Add(WeaponStance.TwoHanded, anim.GetLayerIndex("Two Handed"));
        equipmentStanceLayers.Add(WeaponStance.OneHandedShield, anim.GetLayerIndex("One Handed Shield"));
        equipmentStanceLayers.Add(WeaponStance.Shield, anim.GetLayerIndex("One Handed Shield"));
        equipmentStanceLayers.Add(WeaponStance.Bow, anim.GetLayerIndex("Bow"));
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
        if (primarySlot.CurrentlyEquipped != null)
        {
            CurrentStance = primarySlot.CurrentlyEquipped.stance;
        }
        else if (secondarySlot.CurrentlyEquipped != null)
        {
            CurrentStance = secondarySlot.CurrentlyEquipped.stance;
        }
        // set all animation stance layers to 0 weight
        foreach (var stance in equipmentStanceLayers)
        {
            anim.SetLayerWeight(stance.Value, 0);
        }
        anim.SetLayerWeight(equipmentStanceLayers[CurrentStance], 1);
    }
}
