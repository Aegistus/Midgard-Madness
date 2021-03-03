using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drawing : CombatState
{
    public Drawing(GameObject gameObject) : base(gameObject)
    {
        transitionsTo.Add(new Transition(typeof(Loosing), RangedEquipped, Not(AttackInput)));
        animationHash = Animator.StringToHash("Drawing");
    }

    public override void AfterExecution()
    {
        anim.SetBool(animationHash, false);
    }

    public override void BeforeExecution()
    {
        Debug.Log("Drawing");
        anim.SetBool(animationHash, true);
    }

    public override void DuringExecution()
    {

    }
}
