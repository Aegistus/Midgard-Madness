using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Rolling : OnGroundState
{
    private float timer = 0;
    private float maxTimer = 1.7f;
    private Vector3 inputVelocity;
    private float moveSpeed = 5f;

    public Func<bool> TimerUp => () => timer >= maxTimer;

    public Rolling(GameObject gameObject) : base(gameObject)
    {
        transitionsTo.Add(new Transition(typeof(Idling), TimerUp, Not(Move)));
        transitionsTo.Add(new Transition(typeof(Walking), TimerUp, Move, Not(Run)));
        transitionsTo.Add(new Transition(typeof(Running), TimerUp, Move, Run));
        animationHash = Animator.StringToHash("Rolling");
    }

    public override void AfterExecution()
    {
        anim.SetLayerWeight(fullBodyLayer, 0);
        anim.SetBool(animationHash, false);
    }

    public override void BeforeExecution()
    {
        Debug.Log("Rolling");
        anim.SetLayerWeight(fullBodyLayer, 1);
        anim.SetBool(animationHash, true);
        timer = 0;
    }

    public override void DuringExecution()
    {
        timer += Time.deltaTime;
        inputVelocity = GetAgentMovementInput();
        if (inputVelocity == Vector3.zero)
        {
            inputVelocity = movement.agentModel.forward;
        }
        movement.SetHorizontalVelocity(inputVelocity * moveSpeed);
        RotateAgentModelToDirection(inputVelocity);
        KeepGrounded();
    }
}
