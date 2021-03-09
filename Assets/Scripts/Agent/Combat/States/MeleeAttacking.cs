using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MeleeAttacking : AgentState
{
    private MeleeWeapon primary;
    private MeleeWeapon secondary;
    private int animVariantHash;
    private int animVariantNumber = 3;
    protected int attackAnimationSpeedHash;
    private bool animationFinished = false;

    public MeleeAttacking(GameObject gameObject) : base(gameObject)
    {
        transitionsTo.Add(new Transition(typeof(Idling), () => animationFinished));
        animVariantHash = Animator.StringToHash("AttackVariant");
        attackAnimationSpeedHash = Animator.StringToHash("AttackSpeed");
        animEvents.OnAnimationEvent += CheckAnimationEvent;
    }

    private void CheckAnimationEvent(EventType eventType)
    {
        switch (eventType)
        {
            case EventType.Finish:
                animationFinished = true;
                break;
            case EventType.WeaponSwish:
                AudioManager.instance.PlaySoundGroupAtPosition("Light Swing", transform.position);
                break;
            case EventType.DamageStart:
                primary?.EnterDamageState(1f);
                secondary?.EnterDamageState(1f); 
                break;
            case EventType.DamageEnd:
                primary?.ExitDamageState();
                secondary?.ExitDamageState();
                break;
        }
    }

    public override void AfterExecution()
    {
        anim.SetInteger(animVariantHash, -1);
        primary?.ExitDamageState();
        secondary?.ExitDamageState();
    }

    public override void BeforeExecution()
    {
        Debug.Log("Melee Attack");
        animationFinished = false;
        int variant = UnityEngine.Random.Range(0, animVariantNumber);
        anim.SetInteger(animVariantHash, variant);
        self.SetHorizontalVelocity(self.Velocity * .2f);
        // have weapons enter damage state
        if (weapons.primarySlot.CurrentlyEquipped?.GetType() == typeof(MeleeWeapon))
        {
            primary = (MeleeWeapon)weapons.primarySlot.CurrentlyEquipped;
        }
        else
        {
            primary = null;
        }
        if (weapons.secondarySlot.CurrentlyEquipped?.GetType() == typeof(MeleeWeapon))
        {
            secondary = (MeleeWeapon)weapons.secondarySlot.CurrentlyEquipped;
        }
        else
        {
            secondary = null;
        }
    }

    public override void DuringExecution()
    {
        self.RotateAgentModelToDirection(self.lookDirection.forward);
    }
}
