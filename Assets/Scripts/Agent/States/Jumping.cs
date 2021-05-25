using UnityEngine;

public class Jumping : AgentState
{
    private float JumpForce => agentStats.jumpForce;
    private float AirMoveSpeed => agentStats.airMoveSpeed;
    Vector3 startingVelocity;

    public Jumping(GameObject gameObject) : base(gameObject)
    {
        transitionsTo.Add(new Transition(typeof(Falling), Falling));
        transitionsTo.Add(new Transition(typeof(Idling), OnGround, Not(Rising), Not(Falling)));
    }

    public override void AfterExecution()
    {

    }

    public override void BeforeExecution()
    {
        Debug.Log("Jumping");
        startingVelocity = movement.Velocity;
        movement.SetVerticalVelocity(0);
        movement.AddVerticalVelocity(JumpForce);
        stamina.DepleteStamina(agentStats.jumpCost);
    }

    Vector3 newVelocity;
    public override void DuringExecution()
    {
        newVelocity = GetAgentMovementInput();
        movement.SetHorizontalVelocity(startingVelocity + (newVelocity * AirMoveSpeed));
        movement.RotateAgentModelToDirection(newVelocity);
    }
}
