using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrafingRight : OnGroundState
{
    public StrafingRight(GameObject gameObject) : base(gameObject)
    {
        transitionsTo.Add(new Transition(typeof(Idling), Not(Right)));
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
