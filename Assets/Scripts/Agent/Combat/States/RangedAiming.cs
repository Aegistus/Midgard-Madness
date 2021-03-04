using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedAiming : CombatState
{
    public RangedAiming(GameObject gameObject) : base(gameObject)
    {
        transitionsTo.Add(new Transition(typeof(RangedAttacking), RangedEquipped, Not(AttackInput)));
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
    }

    public override void DuringExecution()
    {

    }
}
