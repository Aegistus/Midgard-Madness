using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Idling : OnGroundState
{

    public Idling(GameObject gameObject) : base(gameObject)
    {
        transitionsTo.Add(new Transition(typeof(Walking), Move));
        transitionsTo.Add(new Transition(typeof(Falling), Not(OnGround)));
        transitionsTo.Add(new Transition(typeof(Blocking), Block, () => vigor.CurrentVigor >= agentStats.blockCost));

        transitionsTo.Add(new Transition(typeof(MeleeAttacking), MeleeEquipped, Attack, () => vigor.CurrentVigor >= agentStats.meleeAttackCost));
        transitionsTo.Add(new Transition(typeof(RangedAiming), RangedEquipped, Attack, () => vigor.CurrentVigor >= agentStats.rangedAimCost));
        transitionsTo.Add(new Transition(typeof(Equipping), EquipWeaponInput));
        transitionsTo.Add(new Transition(typeof(UnEquipping), UnEquipWeapon));
    }

    public override void AfterExecution()
    {
        audio.loop = false;
        audio.Stop();
        if (navAgent)
        {
            navAgent.isStopped = false;
        }
    }

    public override void BeforeExecution()
    {
        Debug.Log("Idling");
        movement.SetHorizontalVelocity(Vector3.zero);
        movement.SetVerticalVelocity(0);
        KeepGrounded();
        if (self.agentSounds)
        {
            audio.clip = self.agentSounds.breathing?.GetRandomAudioClip();
            audio.loop = true;
            audio.Play();
        }
        if (navAgent)
        {
            navAgent.velocity = Vector3.zero;
            navAgent.isStopped = true;
        }
    }

    public override void DuringExecution()
    {
        
    }
}
