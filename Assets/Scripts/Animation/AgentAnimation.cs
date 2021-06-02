using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AnimEventType
{
    Start, Finish, WeaponSound, DamageStart, DamageEnd, Footstep, ChainStart
}

/**
 * Place on the agent's model next to the Animator
 */
[RequireComponent(typeof(Animator))]
public class AgentAnimation : MonoBehaviour
{
    public RuntimeAnimatorController unarmedController;

    public event Action<AnimEventType> OnAnimationEvent;

    Agent agent;
    Animator anim;
    AgentWeapons weapons;
    MultiDictionary<Type, int> fullBodyStates = new MultiDictionary<Type, int>();
    int attackSpeedHash = Animator.StringToHash("AttackSpeed");
    int moveSpeedHash = Animator.StringToHash("MoveSpeed");

    private void Start()
    {
        agent = GetComponentInParent<Agent>();
        anim = GetComponent<Animator>();
        weapons = GetComponentInParent<AgentWeapons>();
        weapons.OnEquippedChange += UpdateAnimatorController;
        fullBodyStates = new MultiDictionary<Type, int>()
        {
            {typeof(Idling), Animator.StringToHash("Idle") },

            {typeof(WalkingForward), Animator.StringToHash("Walk Forward") },
            {typeof(WalkingBackward), Animator.StringToHash("Walk Backward") },
            {typeof(WalkingLeft), Animator.StringToHash("Walk Left") },
            {typeof(WalkingRight), Animator.StringToHash("Walk Right") },

            {typeof(Running), Animator.StringToHash("Run") },
            {typeof(Jumping), Animator.StringToHash("Jump")},
            {typeof(Falling), Animator.StringToHash("Fall") },

            {typeof(Crouching), Animator.StringToHash("Crouch") },
            {typeof(Rolling), Animator.StringToHash("Roll") },
            {typeof(DodgingLeft), Animator.StringToHash("Dodge Left") },
            {typeof(DodgingRight), Animator.StringToHash("Dodge Right") },
            {typeof(DodgingBackward), Animator.StringToHash("Dodge Backward") },

            {typeof(Equipping), Animator.StringToHash("Equip Start") },
            {typeof(UnEquipping), Animator.StringToHash("Equip Start") },

            {typeof(Blocking), Animator.StringToHash("Block") },
            {typeof(RangedAiming), Animator.StringToHash("Ranged Aim") },
            {typeof(RangedAttacking), Animator.StringToHash("Ranged Attack") },
            {typeof(MeleeAttacking), Animator.StringToHash("Attack 00") },
            {typeof(MeleeAttacking), Animator.StringToHash("Attack 01") },
            {typeof(MeleeAttacking), Animator.StringToHash("Attack 02") },
            {typeof(MomentumAttacking), Animator.StringToHash("Momentum Attack 00") },
            {typeof(MomentumAttacking), Animator.StringToHash("Momentum Attack 01") },
            {typeof(MomentumAttacking), Animator.StringToHash("Momentum Attack 02") },
            
            {typeof(TakingDamage), Animator.StringToHash("Impact") },

            {typeof(Dying), Animator.StringToHash("Death 00") },
            {typeof(Dying), Animator.StringToHash("Death 01") },
            {typeof(Dying), Animator.StringToHash("Death 02") },
            {typeof(Dying), Animator.StringToHash("Death 03") },
            {typeof(Dying), Animator.StringToHash("Death 04") },
        };
        agent.StateMachine.OnStateChange += ChangeFullBodyAnimation;
        UpdateAnimatorController();
    }

    private void ChangeFullBodyAnimation(State newState)
    {
        if (fullBodyStates.ContainsKey(newState.GetType()))
        {
            anim.speed = 1;
            anim.CrossFade(fullBodyStates[newState.GetType()], .1f);
        }
        else
        {
            Debug.LogWarning("No Full Body Animation Found For Current Agent State");
        }
    }

    public void UpdateAnimatorController()
    {
        AnimatorOverrideController animController = null;
        if (weapons.primarySlot.CurrentlyEquipped != null)
        {
            animController = weapons.primarySlot.CurrentlyEquipped.stats.weaponAnimationSet;
        }
        else if (weapons.secondarySlot.CurrentlyEquipped != null)
        {
            animController = weapons.secondarySlot.CurrentlyEquipped.stats.weaponAnimationSet;
        }
        // set animation controller to the weapon's controller
        if (animController != null)
        {
            anim.runtimeAnimatorController = animController;
        }
        else
        {
            anim.runtimeAnimatorController = unarmedController;
        }
        anim.SetFloat(attackSpeedHash, agent.agentStats.attackSpeed);
        anim.SetFloat(moveSpeedHash, agent.agentStats.moveSpeed);
    }

    public void CallAnimationEvent(AnimEventType eventType)
    {
        OnAnimationEvent?.Invoke(eventType);
    }

    private void OnAnimatorMove()
    {
        transform.parent.position += anim.deltaPosition;
    }

}
