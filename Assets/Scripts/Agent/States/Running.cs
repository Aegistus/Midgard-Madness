﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Running : OnGroundState
{
    public Running(GameObject gameObject) : base(gameObject)
    {
        transitionsTo.Add(new Transition(typeof(WalkingForward), Not(Run)));
        transitionsTo.Add(new Transition(typeof(Idling), Not(Forward), Not(Run)));
        transitionsTo.Add(new Transition(typeof(WalkingForward), () => stamina.CurrentStamina < agentStats.runCost));
        transitionsTo.Add(new Transition(typeof(MomentumAttacking), MeleeEquipped, Attack, () => vigor.CurrentVigor >= agentStats.momentumAttackCost));
        transitionsTo.Add(new Transition(typeof(RangedAiming), RangedEquipped, Attack, () => vigor.CurrentVigor >= agentStats.rangedAimCost));
        transitionsTo.Add(new Transition(typeof(Equipping), EquipWeaponInput));
        transitionsTo.Add(new Transition(typeof(UnEquipping), UnEquipWeapon));
        transitionsTo.Add(new Transition(typeof(Falling), Not(OnGround)));
        transitionsTo.Add(new Transition(typeof(Blocking), Block, () => vigor.CurrentVigor >= agentStats.blockCost));
    }

    public override void AfterExecution()
    {
        animEvents.OnAnimationEvent -= FootstepEvent;
        audio.Stop();
    }

    public override void BeforeExecution()
    {
        Debug.Log("Running");
        if (self.agentSounds)
        {
            audio.clip = self.agentSounds.heavyBreathing.GetRandomAudioClip();
            audio.loop = true;
            audio.Play();
        }
        //if (navAgent != null)
        //{
        //    navAgent.speed = agentStats.runSpeed;
        //}
        animEvents.OnAnimationEvent += FootstepEvent;
    }

    private void FootstepEvent(EventType obj)
    {
        if (obj == EventType.Footstep && self.agentSounds != null)
        {
            audioManager.PlaySoundAtPosition(self.agentSounds.footsteps, transform.position);
        }
    }

    Vector3 inputVelocity;
    public override void DuringExecution()
    {
        inputVelocity = GetAgentMovementInput();
        movement.RotateAgentModelToDirection(inputVelocity);
        stamina.DepleteStamina(agentStats.runCost * Time.deltaTime);
        KeepGrounded();
    }

}