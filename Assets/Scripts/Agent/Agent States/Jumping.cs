﻿using UnityEngine;

public class Jumping : AgentState
{
    private float jumpForce = 5f;
    private float airMoveSpeed = 3f;
    Vector3 startingVelocity;

    public Jumping(GameObject gameObject) : base(gameObject)
    {
        animationHash = Animator.StringToHash("Jumping");
        transitionsTo.Add(new Transition(typeof(Falling), Falling));
        transitionsTo.Add(new Transition(typeof(Idling), OnGround, Not(Rising), Not(Falling)));
        moveStaminaCost = 10f;
    }

    public override void AfterExecution()
    {
        anim.SetBool(animationHash, false);
    }

    public override void BeforeExecution()
    {
        Debug.Log("Jumping");
        if (HasEnoughMoveStamina())
        {
            anim.SetBool(animationHash, true);
            startingVelocity = self.Velocity;
            self.AddVerticalVelocity(jumpForce);
            stamina.DepleteMoveStamina(moveStaminaCost);
        }
    }

    Vector3 newVelocity;
    public override void DuringExecution()
    {
        newVelocity = GetAgentMovementInput();
        self.SetHorizontalVelocity(startingVelocity + (newVelocity * airMoveSpeed));
        self.RotateAgentModelToDirection(newVelocity);
    }
}
