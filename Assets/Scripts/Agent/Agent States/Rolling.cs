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
    }

    public override void AfterExecution()
    {

    }

    public override void BeforeExecution()
    {
        Debug.Log("Rolling");
        timer = 0;
        stamina.DepleteStamina(agentStats.rollCost);
    }

    public override void DuringExecution()
    {
        timer += Time.deltaTime;
        inputVelocity = GetAgentMovementInput();
        if (inputVelocity == Vector3.zero)
        {
            inputVelocity = movement.agentModel.forward;
        }
        movement.SetHorizontalVelocity(inputVelocity * MoveSpeed);
        movement.RotateAgentModelToDirection(inputVelocity);
        KeepGrounded();
    }
}
