using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkingBackward : OnGroundState
{
    private float MoveSpeed => agentStats.walkSpeed;

    public WalkingBackward(GameObject gameObject) : base(gameObject)
    {
        transitionsTo.Add(new Transition(typeof(Idling), Not(Backward)));
    }

    public override void AfterExecution()
    {

    }

    public override void BeforeExecution()
    {

    }

    Vector3 inputVelocity;
    public override void DuringExecution()
    {
        movement.RotateAgentModelToDirection(movement.lookDirection.forward);
        if (navAgent == null)
        {
            inputVelocity = GetAgentMovementInput();
            movement.SetHorizontalVelocity(inputVelocity * MoveSpeed);
            //movement.RotateAgentModelToDirection(inputVelocity);
        }
        KeepGrounded();
    }
}
