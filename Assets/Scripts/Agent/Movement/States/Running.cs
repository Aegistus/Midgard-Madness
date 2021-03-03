using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Running : OnGroundState
{
    private float moveSpeed = 6f;

    public Running(GameObject gameObject) : base(gameObject)
    {
        animationHash = Animator.StringToHash("Running");
        transitionsTo.Add(new Transition(typeof(Walking), Not(Run)));
        transitionsTo.Add(new Transition(typeof(Idling), Not(Move), Not(Run)));
        transitionsTo.Add(new Transition(typeof(Falling), Not(OnGround)));
        transitionsTo.Add(new Transition(typeof(Sliding), Crouch));
        transitionsTo.Add(new Transition(typeof(Rolling), Roll, Move));
    }

    public override void AfterExecution()
    {
        anim.SetBool(animationHash, false);
    }

    public override void BeforeExecution()
    {
        Debug.Log("Running");
        anim.SetBool(animationHash, true);
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