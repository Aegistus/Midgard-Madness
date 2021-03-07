using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MeleeAttacking : AgentState
{
    protected AgentAnimEvents animEvents;

    private MeleeWeapon primary;
    private MeleeWeapon secondary;
    private int animVariantHash;
    private int animVariantNumber = 3;
    private float timer = 0;
    private float maxTimer = 2f;

    public MeleeAttacking(GameObject gameObject) : base(gameObject)
    {
        transitionsTo.Add(new Transition(typeof(Idling), () => timer >= maxTimer, Not(Attack)));
        animVariantHash = Animator.StringToHash("AttackVariant");
        animEvents = gameObject.GetComponentInChildren<AgentAnimEvents>();
    }

    public override void AfterExecution()
    {
        anim.SetInteger(animVariantHash, -1);
    }

    public override void BeforeExecution()
    {
        Debug.Log("Melee Attack");
        int variant = UnityEngine.Random.Range(0, animVariantNumber);
        anim.SetInteger(animVariantHash, variant);
        timer = 0;
        movement.SetHorizontalVelocity(Vector3.zero);
        // have weapons enter damage state
        if (weapons.primarySlot.CurrentlyEquipped?.GetType() == typeof(MeleeWeapon))
        {
            primary = (MeleeWeapon)weapons.primarySlot.CurrentlyEquipped;
            primary.EnterDamageState(2f);
        }
        else
        {
            primary = null;
        }
        if (weapons.secondarySlot.CurrentlyEquipped?.GetType() == typeof(MeleeWeapon))
        {
            secondary = (MeleeWeapon)weapons.secondarySlot.CurrentlyEquipped;
            secondary.EnterDamageState(2f);
        }
        else
        {
            secondary = null;
        }
    }

    public override void DuringExecution()
    {
        timer += Time.deltaTime;
    }
}
