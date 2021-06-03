using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockingCooldown : AgentState
{
    private float timer = 0;
    private float cooldownTime = 1f;

    public BlockingCooldown(GameObject gameObject) : base(gameObject)
    {
        transitionsTo.Add(new Transition(typeof(Idling), () => timer >= cooldownTime));
        transitionsTo.Add(new Transition(typeof(MeleeAttacking), Attack));
        transitionsTo.Add(new Transition(typeof(WalkingForward), Move));
        transitionsTo.Add(new Transition(typeof(Rolling), Block, Jump, Move));
    }

    public override void AfterExecution()
    {

    }

    public override void BeforeExecution()
    {
        timer = 0;
    }

    public override void DuringExecution()
    {
        timer += Time.deltaTime;
    }
}
