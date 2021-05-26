using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Falling : AgentState
{
    private float airMoveSpeed = 1f;
    Vector3 startingVelocity;

    public Falling(GameObject gameObject) : base(gameObject)
    {
        transitionsTo.Add(new Transition(typeof(Idling), Not(Falling), OnGround));
    }

    public override void AfterExecution()
    {
        movement.SetVerticalVelocity(0f);
    }

    public override void BeforeExecution()
    {
        Debug.Log("Falling");
        startingVelocity = movement.Velocity;
    }

    Vector3 newVelocity;
    public override void DuringExecution()
    {
        movement.AddVerticalVelocity(-9.8f * Time.deltaTime);
        newVelocity = GetAgentMovementInput();
        if (newVelocity.sqrMagnitude > 0)
        {
            movement.SetHorizontalVelocity(startingVelocity + newVelocity * airMoveSpeed);
            movement.RotateAgentModelToDirection(newVelocity);
        }
    }
}
