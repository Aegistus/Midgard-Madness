using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class AgentWeapons : MonoBehaviour
{
    public WeaponSlot primarySlot;
    public WeaponSlot secondarySlot;
    public RuntimeAnimatorController unarmedController;

    public List<Weapon> CarriedWeapons { get; private set; }

    public WeaponStance CurrentStance { get; private set; }

    private Animator anim;
    private int moveSpeedHash = Animator.StringToHash("MoveSpeed");

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
        UpdateWeaponAnimation();
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
        UpdateWeaponAnimation();
    }

    public void EquipWeapon(int numKey)
    {
        if (numKey - 1 < CarriedWeapons.Count && numKey - 1 >= 0)
        {
            Weapon toEquip = CarriedWeapons[numKey - 1];
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
            anim.SetFloat(moveSpeedHash, primarySlot.CurrentlyEquipped.stats.movementMultiplier);
        }
        else if (secondarySlot.CurrentlyEquipped != null)
        {
            CurrentStance = secondarySlot.CurrentlyEquipped.stats.stance;
            animController = secondarySlot.CurrentlyEquipped.stats.weaponAnimationSet;
            anim.SetFloat(moveSpeedHash, secondarySlot.CurrentlyEquipped.stats.movementMultiplier);
        }
        else
        {
            anim.SetFloat(moveSpeedHash, 1);
        }
        // set animation controller to the weapon's controller
        if (animController != null)
        {
            anim.runtimeAnimatorController = animController;
        }
        else
        {
            anim.runtimeAnimatorController = unarmedController;
        }

    }
}
