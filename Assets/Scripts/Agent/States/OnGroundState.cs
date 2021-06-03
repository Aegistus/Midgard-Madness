using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class OnGroundState : AgentState
{
    RaycastHit rayHit;

    protected OnGroundState(GameObject gameObject) : base(gameObject)
    {
        transitionsTo.Add(new Transition(typeof(Crouching), Crouch, Not(Move)));
        transitionsTo.Add(new Transition(typeof(Jumping), Jump, OnGround, Not(Block), () => stamina.CurrentStamina >= agentStats.jumpCost));
        transitionsTo.Add(new Transition(typeof(Rolling), Block, Jump, Move));
        transitionsTo.Add(new Transition(typeof(DodgingLeft), () => self.Dodge, () => self.Left, () => stamina.CurrentStamina >= agentStats.dodgeCost));
        transitionsTo.Add(new Transition(typeof(DodgingRight), () => self.Dodge, () => self.Right, () => stamina.CurrentStamina >= agentStats.dodgeCost));
        transitionsTo.Add(new Transition(typeof(DodgingBackward), () => self.Dodge, () => self.Backwards, () => stamina.CurrentStamina >= agentStats.dodgeCost));
    }

    protected void KeepGrounded()
    {
        if (OnGround() && Not(Jump)())
        {
            movement.SetVerticalVelocity(-5f);
        }
    }
}
