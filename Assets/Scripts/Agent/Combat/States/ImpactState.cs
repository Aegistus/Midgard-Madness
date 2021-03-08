using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImpactState : AgentState
{
    float timer = 0;
    float maxTimer = 1f;

    public ImpactState(GameObject gameObject) : base(gameObject)
    {
        animationHash = Animator.StringToHash("Impact");
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
        Debug.Log("Impact State");
        movement.SetHorizontalVelocity(Vector3.zero);
    }

    public override void DuringExecution()
    {
        timer -= Time.deltaTime;
    }
}
