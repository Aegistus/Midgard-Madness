using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum AnimationStance
{
    TwoHanded, OneHandedShield, Bow
}

public class AgentWeapons : MonoBehaviour
{
    public WeaponSlot primarySlot;
    public WeaponSlot secondarySlot;
    public List<Weapon> carriedPrimaryWeapons;
    public List<Weapon> carriedSecondaryWeapons;

    public AnimationStance CurrentStance { get; private set; }

    private Queue<Weapon> primaryEquipQueue = new Queue<Weapon>();
    private Queue<Weapon> secondaryEquipQueue = new Queue<Weapon>();

    private Animator anim;
    private Dictionary<AnimationStance, int> equipmentStanceLayers = new Dictionary<AnimationStance, int>();

    private void Start()
    {
        anim = GetComponentInChildren<Animator>();
        equipmentStanceLayers.Add(AnimationStance.TwoHanded, anim.GetLayerIndex("Two Handed"));
        equipmentStanceLayers.Add(AnimationStance.OneHandedShield, anim.GetLayerIndex("One Handed Shield"));
        equipmentStanceLayers.Add(AnimationStance.Bow, anim.GetLayerIndex("Bow"));
        foreach (var primaryEquip in carriedPrimaryWeapons)
        {
            primaryEquipQueue.Enqueue(primaryEquip);
        }
        foreach (var secondaryEquip in carriedSecondaryWeapons)
        {
            secondaryEquipQueue.Enqueue(secondaryEquip);
        }
    }

    public void GoToNextPrimaryEquipment()
    {
        if (primarySlot.CurrentlyEquipped != null)
        {
            primaryEquipQueue.Enqueue(primarySlot.UnEquip());
        }
        if (primaryEquipQueue.Count > 0)
        {
            primarySlot.Equip(primaryEquipQueue.Dequeue());
            UpdateWeaponAnimation();
        }
        // if primary uses both hands, unequip secondary
        if (primarySlot.CurrentlyEquipped?.usage == Weapon.Usage.Both)
        {
            secondaryEquipQueue.Enqueue(secondarySlot.UnEquip());
        }
    }

    public void GoToNextSecondaryEquipment()
    {
        if (primarySlot.CurrentlyEquipped?.usage != Weapon.Usage.Both)
        {
            if (secondarySlot.CurrentlyEquipped != null)
            {
                secondaryEquipQueue.Enqueue(secondarySlot.UnEquip());
            }
            if (secondaryEquipQueue.Count > 0)
            {
                secondarySlot.Equip(secondaryEquipQueue.Dequeue());
                UpdateWeaponAnimation();
            }
        }
    }

    public void UpdateWeaponAnimation()
    {
        if (primarySlot.CurrentlyEquipped?.GetType() == typeof(MeleeWeapon))
        {
            if (primarySlot.CurrentlyEquipped?.usage == Weapon.Usage.Both)
            {
                CurrentStance = AnimationStance.TwoHanded;
            }
            else
            {
                CurrentStance = AnimationStance.OneHandedShield;
            }
        }
        else
        {
            if (primarySlot.CurrentlyEquipped?.usage == Weapon.Usage.Both)
            {
                CurrentStance = AnimationStance.Bow;
            }
        }

        // set all animation stance layers to 0 weight
        foreach (var stance in equipmentStanceLayers)
        {
            anim.SetLayerWeight(stance.Value, 0);
        }
        anim.SetLayerWeight(equipmentStanceLayers[CurrentStance], 1);
    }
}
