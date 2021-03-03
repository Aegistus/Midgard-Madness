using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Sliding : MovementState
{
    Func<bool> TimerUp => () => timer >= timerMax;

    float timerMax = 1.1f;
    float timer;

    float slideSpeed = 8f;

    public Sliding(GameObject gameObject) : base(gameObject)
    {
        animationHash = Animator.StringToHash("Sliding");
        transitionsTo.Add(new Transition(typeof(Running), TimerUp));
    }

    public override void AfterExecution()
    {
        anim.SetLayerWeight(fullBodyLayer, 0);
        anim.SetBool(animationHash, false);
    }

    public override void BeforeExecution()
    {
        anim.SetLayerWeight(fullBodyLayer, 1);
        anim.SetBool(animationHash, true);
        movement.SetHorizontalVelocity(movement.agentModel.forward * slideSpeed);
        timer = 0;
    }

    public override void DuringExecution()
    {
        timer += Time.deltaTime;
    }
}
