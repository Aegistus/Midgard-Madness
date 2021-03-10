using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedAiming : AgentState
{
    public RangedAiming(GameObject gameObject) : base(gameObject)
    {
        transitionsTo.Add(new Transition(typeof(RangedAttacking), RangedEquipped, Not(Attack)));
        animationHash = Animator.StringToHash("RangedAim");
    }

    public override void AfterExecution()
    {
        anim.SetBool(animationHash, false);
    }

    public override void BeforeExecution()
    {
        Debug.Log("Aiming");
        anim.SetBool(animationHash, true);
        self.SetHorizontalVelocity(Vector3.zero);
    }

    public override void DuringExecution()
    {
        self.RotateAgentModelToDirection(self.lookDirection.forward);
    }
}
