using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MomentumAttacking : AgentState
{
    private MeleeWeapon primary;
    private MeleeWeapon secondary;
    private int animVariantHash;
    private int animVariantNumber = 3;
    protected int attackAnimationSpeedHash;
    private float timer = 0;

    public MomentumAttacking(GameObject gameObject) : base(gameObject)
    {
        transitionsTo.Add(new Transition(typeof(Idling), () => timer <= 0));
        animVariantHash = Animator.StringToHash("MomentumAttackVariant");
        attackAnimationSpeedHash = Animator.StringToHash("AttackSpeed");
    }

    public override void AfterExecution()
    {
        anim.SetInteger(animVariantHash, -1);
    }

    public override void BeforeExecution()
    {
        Debug.Log("Momentum Attack");
        int variant = Random.Range(0, animVariantNumber);
        anim.SetInteger(animVariantHash, variant);
        movement.SetHorizontalVelocity(movement.Velocity * .8f);
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
        AnimatorStateInfo attackClip = anim.GetCurrentAnimatorStateInfo(0);
        timer = attackClip.length / attackClip.speedMultiplier;
        Debug.Log(attackClip.length);
    }

    public override void DuringExecution()
    {
        timer -= Time.deltaTime;
    }
}
