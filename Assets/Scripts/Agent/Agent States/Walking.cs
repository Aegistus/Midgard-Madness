using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walking : OnGroundState
{
    private float moveSpeed = 3f;

    public Walking(GameObject gameObject) : base(gameObject)
    {
        animationHash = Animator.StringToHash("Walking");
        transitionsTo.Add(new Transition(typeof(Idling), Not(Move)));
        transitionsTo.Add(new Transition(typeof(MeleeAttacking), MeleeEquipped, Attack));
        transitionsTo.Add(new Transition(typeof(RangedAiming), RangedEquipped, Attack));
        transitionsTo.Add(new Transition(typeof(Equipping), EquipWeaponInput));
        transitionsTo.Add(new Transition(typeof(Running), Run));
        transitionsTo.Add(new Transition(typeof(Falling), Not(OnGround)));
        transitionsTo.Add(new Transition(typeof(Blocking), Block));
    }

    public override void AfterExecution()
    {
        anim.SetBool(animationHash, false);
    }

    public override void BeforeExecution()
    {
        anim.SetBool(animationHash, true);
        self.SetHorizontalVelocity(Vector3.zero);
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
