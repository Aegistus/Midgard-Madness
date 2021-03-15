using UnityEngine;

public class Jumping : AgentState
{
    private float JumpForce => agentStats.jumpForce;
    private float AirMoveSpeed => agentStats.airMoveSpeed;
    Vector3 startingVelocity;

    public Jumping(GameObject gameObject) : base(gameObject)
    {
        animationHash = Animator.StringToHash("Jumping");
        transitionsTo.Add(new Transition(typeof(Falling), Falling));
        transitionsTo.Add(new Transition(typeof(Idling), OnGround, Not(Rising), Not(Falling)));
    }

    public override void AfterExecution()
    {
        anim.SetBool(animationHash, false);
    }

    public override void BeforeExecution()
    {
        Debug.Log("Jumping");
        anim.SetBool(animationHash, true);
        startingVelocity = self.Velocity;
        self.SetVerticalVelocity(0);
        self.AddVerticalVelocity(JumpForce);
        stamina.DepleteStamina(agentStats.jumpCost);
    }

    Vector3 newVelocity;
    public override void DuringExecution()
    {
        newVelocity = GetAgentMovementInput();
        self.SetHorizontalVelocity(startingVelocity + (newVelocity * AirMoveSpeed));
        self.RotateAgentModelToDirection(newVelocity);
    }
}
