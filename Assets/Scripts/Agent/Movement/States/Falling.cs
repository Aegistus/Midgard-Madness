using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Falling : AgentState
{
    private float airMoveSpeed = 1f;
    Vector3 startingVelocity;

    public Falling(GameObject gameObject) : base(gameObject)
    {
        animationHash = Animator.StringToHash("Falling");
        transitionsTo.Add(new Transition(typeof(Idling), Not(Falling), OnGround));
    }

    public override void AfterExecution()
    {
        anim.SetBool(animationHash, false);
    }

    public override void BeforeExecution()
    {
        Debug.Log("Falling");
        startingVelocity = movement.Velocity;
        anim.SetBool(animationHash, true);
    }

    Vector3 newVelocity;
    public override void DuringExecution()
    {
        newVelocity = GetAgentMovementInput();
        if (newVelocity.sqrMagnitude > 0)
        {
            movement.SetHorizontalVelocity(startingVelocity + newVelocity * airMoveSpeed);
            movement.RotateAgentModelToDirection(newVelocity);
        }
    }
}
