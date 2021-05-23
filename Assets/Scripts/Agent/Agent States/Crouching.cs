using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crouching : AgentState
{
    float crouchHeight = .5f;

    public Crouching(GameObject gameObject) : base(gameObject)
    {
        transitionsTo.Add(new Transition(typeof(Idling), Not(Crouch)));
    }

    public override void AfterExecution()
    {
        //transform.position += Vector3.down * crouchHeight;
    }

    public override void BeforeExecution()
    {
        Debug.Log("Crouching");
        self.SetHorizontalVelocity(Vector3.zero);
        //transform.position -= Vector3.down * crouchHeight;
    }

    public override void DuringExecution()
    {

    }
}
