using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkingBackward : OnGroundState
{
    public WalkingBackward(GameObject gameObject) : base(gameObject)
    {
        transitionsTo.Add(new Transition(typeof(Idling), Not(Backward)));
    }

    public override void AfterExecution()
    {

    }

    public override void BeforeExecution()
    {

    }

    public override void DuringExecution()
    {
        movement.RotateAgentModelToDirection(movement.lookDirection.forward);
    }
}
