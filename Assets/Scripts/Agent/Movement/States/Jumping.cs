using UnityEngine;

public class Jumping : MovementState
{
    private float jumpForce = 5f;
    private float airMoveSpeed = 2f;
    Vector3 startingVelocity;

    public Jumping(GameObject gameObject) : base(gameObject)
    {
        animationHash = Animator.StringToHash("Jumping");
        transitionsTo.Add(new Transition(typeof(Falling), Falling));
        transitionsTo.Add(new Transition(typeof(Idling), OnGround, Not(Rising), Not(Falling)));
    }

    public override void AfterExecution()
    {
        anim.SetLayerWeight(fullBodyLayer, 0);
        anim.SetBool(animationHash, false);
    }

    public override void BeforeExecution()
    {
        Debug.Log("Jumping");
        anim.SetLayerWeight(fullBodyLayer, 1);
        anim.SetBool(animationHash, true);
        startingVelocity = movement.velocity * .75f;
        movement.AddVerticalVelocity(jumpForce);
    }

    Vector3 newVelocity;
    public override void DuringExecution()
    {
        newVelocity = GetAgentMovementInput();
        movement.SetHorizontalVelocity(startingVelocity + newVelocity * airMoveSpeed);
        RotateAgentModelToDirection(newVelocity);
    }
}
