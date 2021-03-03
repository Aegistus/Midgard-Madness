using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Loosing : CombatState
{
    private float timer = 0;
    private float timerMax = 3f;

    public Func<bool> TimerUp => () => timer >= timerMax;

    public Loosing(GameObject gameObject) : base(gameObject)
    {
        transitionsTo.Add(new Transition(typeof(ReadyState), TimerUp));
        animationHash = Animator.StringToHash("Loosing");
    }

    public override void AfterExecution()
    {
        anim.SetBool(animationHash, false);
    }

    public override void BeforeExecution()
    {
        Debug.Log("Loosing");
        anim.SetBool(animationHash, true);
    }

    public override void DuringExecution()
    {
        timer += Time.deltaTime;
    }
}
