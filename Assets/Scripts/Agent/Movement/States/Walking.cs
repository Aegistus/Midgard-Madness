using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walking : OnGroundState
{
    private float moveSpeed = 3f;
    private int strafeLeftHash = Animator.StringToHash("StrafeLeft");
    private int strafeRightHash = Animator.StringToHash("StrafeRight");

    public Walking(GameObject gameObject) : base(gameObject)
    {
        animationHash = Animator.StringToHash("Walking");
        transitionsTo.Add(new Transition(typeof(Idling), Not(Move)));
        transitionsTo.Add(new Transition(typeof(Running), Run));
        transitionsTo.Add(new Transition(typeof(Falling), Not(OnGround)));
        transitionsTo.Add(new Transition(typeof(Rolling), Roll, Move));
    }

    public override void AfterExecution()
    {
        //if (movement.velocity.y > 0)
        //{
        //    movement.AddVerticalVelocity(0);
        //}
        anim.SetBool(animationHash, false);
    }

    public override void BeforeExecution()
    {
        anim.SetBool(animationHash, true);
        movement.SetHorizontalVelocity(Vector3.zero);
    }

    Vector3 inputVelocity;
    public override void DuringExecution()
    {
        inputVelocity = GetAgentMovementInput();
        movement.SetHorizontalVelocity(inputVelocity * moveSpeed);
        RotateAgentModelToDirection(inputVelocity);
        KeepGrounded();
    }

}
