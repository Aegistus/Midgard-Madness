using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MomentumAttacking : AgentState
{
    private MeleeWeapon primary;
    private MeleeWeapon secondary;
    protected int attackAnimationSpeedHash;
    private bool animationFinished = false;

    public MomentumAttacking(GameObject gameObject) : base(gameObject)
    {
        transitionsTo.Add(new Transition(typeof(Idling), () => animationFinished));
        attackAnimationSpeedHash = Animator.StringToHash("AttackSpeed");
        animEvents.OnAnimationEvent += CheckAnimationEvent;
    }

    private void CheckAnimationEvent(EventType eventType)
    {
        if (eventType == EventType.Finish)
        {
            animationFinished = true;
        }
        else if (eventType == EventType.WeaponSound)
        {
            audioManager.PlaySoundAtPosition("Heavy Swing", transform.position);
        }
    }

    public override void AfterExecution()
    {

    }

    public override void BeforeExecution()
    {
        Debug.Log("Momentum Attack");
        animationFinished = false;
        self.SetHorizontalVelocity(self.Velocity * .5f);
        // have weapons enter damage state
        if (weapons.primarySlot.CurrentlyEquipped?.GetType() == typeof(MeleeWeapon))
        {
            primary = (MeleeWeapon)weapons.primarySlot.CurrentlyEquipped;
            primary.EnterDamageState(3f, 2f);
        }
        else
        {
            primary = null;
        }
        if (weapons.secondarySlot.CurrentlyEquipped?.GetType() == typeof(MeleeWeapon))
        {
            secondary = (MeleeWeapon)weapons.secondarySlot.CurrentlyEquipped;
            secondary.EnterDamageState(3f, 2f);
        }
        else
        {
            secondary = null;
        }
        vigor.DepleteVigor(agentStats.momentumAttackCost);
    }

    public override void DuringExecution()
    {
        self.RotateAgentModelToDirection(self.lookDirection.forward, .1f);
    }
}
