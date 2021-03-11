using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Rolling : OnGroundState
{
    private float timer = 0;
    private float maxTimer = 1.7f;
    private Vector3 inputVelocity;
    private float MoveSpeed => agentStats.rollSpeed;

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
        anim.SetBool(animationHash, false);
    }

    public override void BeforeExecution()
    {
        Debug.Log("Rolling");
        anim.SetBool(animationHash, true);
        timer = 0;
        stamina.DepleteStamina(agentStats.rollCost);
    }

    public override void DuringExecution()
    {
        timer += Time.deltaTime;
        inputVelocity = GetAgentMovementInput();
        if (inputVelocity == Vector3.zero)
        {
            inputVelocity = self.agentModel.forward;
        }
        self.SetHorizontalVelocity(inputVelocity * MoveSpeed);
        self.RotateAgentModelToDirection(inputVelocity);
        KeepGrounded();
    }
}
