using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class OnGroundState : MovementState
{
    RaycastHit rayHit;

    protected OnGroundState(GameObject gameObject) : base(gameObject)
    {
        transitionsTo.Add(new Transition(typeof(Crouching), Crouch, Not(Move)));
        transitionsTo.Add(new Transition(typeof(Climbing), Jump, FacingHighWall, LedgeInReach));
        transitionsTo.Add(new Transition(typeof(Vaulting), Jump, Not(FacingHighWall), FacingLowWall, OtherSideOfVaultOpen));
        transitionsTo.Add(new Transition(typeof(Jumping), Jump, OnGround, Not(FacingHighWall)));
    }

    protected void KeepGrounded()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out rayHit, .75f, groundLayer) && Not(Jump)())
        {
            movement.SetVerticalVelocity(-10f);
        }
    }
}
