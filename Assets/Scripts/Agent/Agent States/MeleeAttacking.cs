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
    private float timer = 0;
    private float canAttackAgainTime = .75f;

    public MeleeAttacking(GameObject gameObject) : base(gameObject)
    {
        transitionsTo.Add(new Transition(typeof(Idling), () => animationFinished));
        Transition attackCombo = new Transition(typeof(MeleeAttacking), () => timer >= canAttackAgainTime, Attack, () => vigor.CurrentVigor >= agentStats.meleeAttackCost)
        {
            CanTransitionToSelf = true
        };
        transitionsTo.Add(attackCombo);
        transitionsTo.Add(new Transition(typeof(Blocking), () => timer >= canAttackAgainTime, Block));
        animVariantHash = Animator.StringToHash("AttackVariant");
        attackAnimationSpeedHash = Animator.StringToHash("AttackSpeed");
        animEvents.OnAnimationEvent += CheckAnimationEvent;
    }

    private void CheckAnimationEvent(EventType eventType)
    {
        if (isCurrentState)
        {
            switch (eventType)
            {
                case EventType.Finish:
                    animationFinished = true;
                    break;
                case EventType.WeaponSound:
                    AudioManager.instance.PlaySoundAtPosition("Light Swing", transform.position);
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
    }

    public override void AfterExecution()
    {
        isCurrentState = false;
        anim.SetInteger(animVariantHash, -1);
        audio.Stop();
        primary?.ExitDamageState();
        secondary?.ExitDamageState();
        timer = 0;
    }

    public override void BeforeExecution()
    {
        Debug.Log("Melee Attack");
        isCurrentState = true;
        animationFinished = false;
        int variant = UnityEngine.Random.Range(0, animVariantNumber);
        anim.SetInteger(animVariantHash, variant);
        anim.SetFloat(attackAnimationSpeedHash, self.agentStats.attackSpeed);
        if (self.agentSounds)
        {
            audio.clip = self.agentSounds.attack.GetRandomAudioClip();
            audio.loop = false;
            audio.Play();
        }

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
        timer = 0;
        vigor.DepleteVigor(agentStats.meleeAttackCost);
    }

    public override void DuringExecution()
    {
        self.RotateAgentModelToDirection(self.lookDirection.forward);
        timer += Time.deltaTime;
    }
}
