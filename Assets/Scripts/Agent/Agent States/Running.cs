using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Running : OnGroundState
{
    private float moveSpeed = 6f;

    public Running(GameObject gameObject) : base(gameObject)
    {
        animationHash = Animator.StringToHash("Running");
        transitionsTo.Add(new Transition(typeof(Walking), Not(Run)));
        transitionsTo.Add(new Transition(typeof(Idling), Not(Move), Not(Run)));
        transitionsTo.Add(new Transition(typeof(MomentumAttacking), MeleeEquipped, Attack));
        transitionsTo.Add(new Transition(typeof(RangedAiming), RangedEquipped, Attack));
        transitionsTo.Add(new Transition(typeof(Equipping), EquipWeaponInput));
        transitionsTo.Add(new Transition(typeof(Falling), Not(OnGround)));
        transitionsTo.Add(new Transition(typeof(Sliding), Crouch));
    }

    public override void AfterExecution()
    {
        anim.SetBool(animationHash, false);
    }

    public override void BeforeExecution()
    {
        Debug.Log("Running");
        anim.SetBool(animationHash, true);
        if (navAgent != null)
        {
            navAgent.speed = moveSpeed;
        }
    }

    Vector3 inputVelocity;
    public override void DuringExecution()
    {
        if (navAgent == null)
        {
            inputVelocity = GetAgentMovementInput();
            self.SetHorizontalVelocity(inputVelocity * moveSpeed);
            self.RotateAgentModelToDirection(inputVelocity);
            KeepGrounded();
        }
    }

}