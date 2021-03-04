using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class RangedAttacking : CombatState
{
    private float timer = 0;
    private float timerMax = 1f;

    public RangedAttacking(GameObject gameObject) : base(gameObject)
    {
        transitionsTo.Add(new Transition(typeof(ReadyState), () => timer >= timerMax));
        animationHash = Animator.StringToHash("RangedAttack");
    }

    public override void AfterExecution()
    {
        anim.SetBool(animationHash, false);
    }

    public override void BeforeExecution()
    {
        Debug.Log("Shooting");
        anim.SetBool(animationHash, true);
        timer = 0;
    }

    public override void DuringExecution()
    {
        timer += Time.deltaTime;
    }
}
