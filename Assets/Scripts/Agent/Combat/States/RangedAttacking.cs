using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class RangedAttacking : AgentState
{
    private bool animationFinished = false;
    private RangedWeapon weapon;

    public RangedAttacking(GameObject gameObject) : base(gameObject)
    {
        transitionsTo.Add(new Transition(typeof(Idling), () => animationFinished));
        animationHash = Animator.StringToHash("RangedAttack");
        animEvents.OnAnimationEvent += CheckAnimationEvent;
    }

    void CheckAnimationEvent(EventType eventType)
    {
        if (eventType == EventType.Finish)
        {
            animationFinished = true;
        }
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
            weapon = (RangedWeapon)weapons.primarySlot.CurrentlyEquipped;
        }
        else if (weapons.secondarySlot.CurrentlyEquipped?.GetType() == typeof(RangedWeapon))
        {
            weapon = (RangedWeapon)weapons.secondarySlot.CurrentlyEquipped;
        }
        anim.SetBool(animationHash, true);
        self.SetHorizontalVelocity(Vector3.zero);
        if (weapon != null)
        {
            weapon.RangedAttack();
        }
    }

    public override void DuringExecution()
    {

    }
}
