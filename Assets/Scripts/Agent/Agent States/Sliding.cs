using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Sliding : AgentState
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
        anim.SetBool(animationHash, false);
    }

    public override void BeforeExecution()
    {
        anim.SetBool(animationHash, true);
        self.SetHorizontalVelocity(self.agentModel.forward * slideSpeed);
        timer = 0;
    }

    public override void DuringExecution()
    {
        timer += Time.deltaTime;
    }
}
