using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class RangedAttacking : AgentState
{
    private bool animationFinished = false;
    private RangedWeaponStats stats;

    public RangedAttacking(GameObject gameObject) : base(gameObject)
    {
        transitionsTo.Add(new Transition(typeof(Idling), () => animationFinished));
        animationHash = Animator.StringToHash("RangedAttack");
    }

    public override void AfterExecution()
    {
        anim.SetBool(animationHash, false);
    }

    public override void BeforeExecution()
    {
        Debug.Log("Shooting");
        if (weapons.primarySlot.CurrentlyEquipped?.GetType() == typeof(RangedWeapon))
        {
            stats = (RangedWeaponStats)weapons.primarySlot.CurrentlyEquipped.stats;
        }
        else if (weapons.primarySlot.CurrentlyEquipped?.GetType() == typeof(RangedWeapon))
        {
            stats = (RangedWeaponStats)weapons.secondarySlot.CurrentlyEquipped.stats;
        }
        anim.SetBool(animationHash, true);
        self.SetHorizontalVelocity(Vector3.zero);
        
    }

    public override void DuringExecution()
    {

    }
}
