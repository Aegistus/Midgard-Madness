using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Falling : MovementState
{
    private float airMoveSpeed = .01f;
    Vector3 startingVelocity;

    public Falling(GameObject gameObject) : base(gameObject)
    {
        animationHash = Animator.StringToHash("Falling");
        transitionsTo.Add(new Transition(typeof(Idling), Not(Falling), OnGround));
    }

    public override void AfterExecution()
    {
        anim.SetLayerWeight(fullBodyLayer, 0);
        anim.SetBool(animationHash, false);
    }

    public override void BeforeExecution()
    {
        Debug.Log("Falling");
        startingVelocity = movement.velocity;
        anim.SetLayerWeight(fullBodyLayer, 1);
        anim.SetBool(animationHash, true);
    }

    Vector3 newVelocity;
    public override void DuringExecution()
    {
        newVelocity = GetAgentMovementInput();
        if (newVelocity.sqrMagnitude > 0)
        {
            movement.SetHorizontalVelocity(startingVelocity + newVelocity * airMoveSpeed);
            RotateAgentModelToDirection(newVelocity);
        }
    }
}
