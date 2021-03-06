using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MeleeAttacking : AgentState
{
    protected AgentAnimEvents animEvents;

    private bool animationFinished = false;
    private MeleeWeapon primary;
    private MeleeWeapon secondary;

    public MeleeAttacking(GameObject gameObject) : base(gameObject)
    {
        transitionsTo.Add(new Transition(typeof(Idling), () => animationFinished, Not(Attack)));
        animationHash = Animator.StringToHash("MeleeAttack");
        animEvents = gameObject.GetComponentInChildren<AgentAnimEvents>();
        animEvents.OnAnimationEvent += CheckAnimationEvent;
    }

    private void CheckAnimationEvent(EventType eventType)
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
        Debug.Log("Melee Attack");
        anim.SetBool(animationHash, true);
        animationFinished = false;
        movement.SetHorizontalVelocity(Vector3.zero);
        if (weapons.primarySlot.CurrentlyEquipped?.GetType() == typeof(MeleeWeapon))
        {
            primary = (MeleeWeapon)weapons.primarySlot.CurrentlyEquipped;
            Debug.Log("Setting weapon damage state");
            primary.EnterDamageState(2f);
        }
        else
        {
            primary = null;
        }
        if (weapons.secondarySlot.CurrentlyEquipped?.GetType() == typeof(MeleeWeapon))
        {
            secondary = (MeleeWeapon)weapons.secondarySlot.CurrentlyEquipped;
            Debug.Log("Setting weapon damage state");
            secondary.EnterDamageState(2f);
        }
        else
        {
            secondary = null;
        }
    }

    public override void DuringExecution()
    {

    }
}
