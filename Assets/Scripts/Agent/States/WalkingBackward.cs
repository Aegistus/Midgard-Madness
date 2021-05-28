using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkingBackward : OnGroundState
{
    private float MoveSpeed => agentStats.walkSpeed;

    public WalkingBackward(GameObject gameObject) : base(gameObject)
    {
        transitionsTo.Add(new Transition(typeof(Idling), Not(Backward)));
        transitionsTo.Add(new Transition(typeof(MeleeAttacking), MeleeEquipped, Attack, () => vigor.CurrentVigor >= agentStats.meleeAttackCost));
        transitionsTo.Add(new Transition(typeof(RangedAiming), RangedEquipped, Attack, () => vigor.CurrentVigor >= agentStats.rangedAimCost));
        transitionsTo.Add(new Transition(typeof(Equipping), EquipWeaponInput));
        transitionsTo.Add(new Transition(typeof(UnEquipping), UnEquipWeapon));
        transitionsTo.Add(new Transition(typeof(Running), Run, () => stamina.CurrentStamina >= agentStats.runCost));
        transitionsTo.Add(new Transition(typeof(Falling), Not(OnGround)));
        transitionsTo.Add(new Transition(typeof(Blocking), Block, () => vigor.CurrentVigor >= agentStats.blockCost));
    }

    public override void AfterExecution()
    {
        animEvents.OnAnimationEvent -= FootstepEvent;
    }

    public override void BeforeExecution()
    {
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
        //movement.SetHorizontalVelocity(inputVelocity * MoveSpeed);
        movement.RotateAgentModelToDirection(-inputVelocity);
        KeepGrounded();
    }
}
