using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class AgentWeapons : MonoBehaviour
{
    public WeaponSlot primarySlot;
    public WeaponSlot secondarySlot;

    public event Action OnEquippedChange;
    public List<Weapon> CarriedWeapons { get; private set; }

    private Animator anim;

    private void Start()
    {
        anim = GetComponentInChildren<Animator>();
        CarriedWeapons = GetComponentsInChildren<Weapon>().ToList();
        foreach (var weapon in CarriedWeapons)
        {
            weapon.gameObject.SetActive(false);
        }
    }

    public bool HasWeaponEquipped()
    {
        return primarySlot.CurrentlyEquipped != null || secondarySlot.CurrentlyEquipped != null;
    }

    public void UnEquipAll()
    {
        primarySlot.UnEquip();
        secondarySlot.UnEquip();
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
            case WeaponStance.BattleAxe:
                secondarySlot.UnEquip();
                primarySlot.Equip(toEquip);
                break;
            default:
                primarySlot.UnEquip();
                primarySlot.Equip(toEquip);
                secondarySlot.UnEquip();
                break;
        }
        OnEquippedChange?.Invoke();
    }

    public void EquipWeapon(int numKey)
    {
        if (numKey - 1 < CarriedWeapons.Count && numKey - 1 >= 0)
        {
            Weapon toEquip = CarriedWeapons[numKey - 1];
            EquipWeapon(toEquip);
        }
    }


}
