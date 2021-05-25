using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crouching : AgentState
{
    public Crouching(GameObject gameObject) : base(gameObject)
    {
        transitionsTo.Add(new Transition(typeof(Idling), Not(Crouch)));
    }

    public override void AfterExecution()
    {

    }

    public override void BeforeExecution()
    {
        Debug.Log("Crouching");
        movement.SetHorizontalVelocity(Vector3.zero);
    }

    public override void DuringExecution()
    {

    }
}
