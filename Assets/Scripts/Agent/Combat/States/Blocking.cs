using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blocking : CombatState
{
    public Blocking(GameObject gameObject) : base(gameObject)
    {
        animationHash = Animator.StringToHash("Blocking");
        transitionsTo.Add(new Transition(typeof(ReadyState), Not(BlockInput)));
    }

    public override void AfterExecution()
    {
        anim.SetBool(animationHash, false);
    }

    public override void BeforeExecution()
    {
        Debug.Log("Blocking");
        anim.SetBool(animationHash, true);
    }

    public override void DuringExecution()
    {

    }
}
