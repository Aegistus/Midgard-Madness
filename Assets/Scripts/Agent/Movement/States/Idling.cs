using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Idling : OnGroundState
{

    public Idling(GameObject gameObject) : base(gameObject)
    {
        animationHash = Animator.StringToHash("Idling");
        transitionsTo.Add(new Transition(typeof(Walking), Move, Not(Attacking)));
        transitionsTo.Add(new Transition(typeof(Falling), Not(OnGround)));
        transitionsTo.Add(new Transition(typeof(Rolling), Roll, Move));
    }

    public override void AfterExecution()
    {
        anim.SetBool(animationHash, false);
    }

    public override void BeforeExecution()
    {
        Debug.Log("Idling");
        anim.SetBool(animationHash, true);
        movement.SetHorizontalVelocity(Vector3.zero);
        movement.SetVerticalVelocity(0);
        KeepGrounded();
    }

    public override void DuringExecution()
    {
        
    }
}
