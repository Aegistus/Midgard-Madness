using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class CombatState : State
{
    protected AgentController agentController;

    protected Animator anim;
    protected AgentWeapons weapons;
    protected int animationHash;

    public Func<bool> MeleeEquipped => () => weapons.primarySlot.CurrentlyEquipped?.GetType() == typeof(MeleeWeapon);
    public Func<bool> RangedEquipped => () => weapons.primarySlot.CurrentlyEquipped?.GetType() == typeof(RangedWeapon) || weapons.secondarySlot.CurrentlyEquipped?.GetType() == typeof(RangedWeapon);
    public Func<bool> AttackInput => () => agentController.Attack;
    public Func<bool> BlockInput => () => agentController.Block;
    public Func<bool> EquipWeaponInput => () => agentController.Equipping;

    protected CombatState(GameObject gameObject) : base(gameObject)
    {
        agentController = gameObject.GetComponent<AgentController>();
        anim = gameObject.GetComponentInChildren<Animator>();
        weapons = gameObject.GetComponent<AgentWeapons>();
    }
}
