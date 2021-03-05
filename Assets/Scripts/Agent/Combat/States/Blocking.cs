using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blocking : AgentState
{
    public Blocking(GameObject gameObject) : base(gameObject)
    {
        animationHash = Animator.StringToHash("Blocking");
        transitionsTo.Add(new Transition(typeof(Idling), Not(Block)));
    }

    public override void AfterExecution()
    {
        anim.SetBool(animationHash, false);
    }

    public override void BeforeExecution()
    {
        Debug.Log("Blocking");
        anim.SetBool(animationHash, true);
        movement.SetHorizontalVelocity(Vector3.zero);
    }

    public override void DuringExecution()
    {

    }
}
