using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakingDamage : AgentState
{
    float timer = 0;
    float maxTimer = 1f;

    public TakingDamage(GameObject gameObject) : base(gameObject)
    {
        animationHash = Animator.StringToHash("TakingDamage");
        transitionsTo.Add(new Transition(typeof(Idling), () => timer <= 0));
    }

    public override void AfterExecution()
    {
        anim.SetBool(animationHash, false);
    }

    public override void BeforeExecution()
    {
        timer = maxTimer;
        anim.SetBool(animationHash, true);
        Debug.Log("Taking Damage");
        self.SetHorizontalVelocity(self.Velocity * .1f);
    }

    public override void DuringExecution()
    {
        timer -= Time.deltaTime;
    }
}
