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
        transitionsTo.Add(new Transition(typeof(Walking), Move));
        transitionsTo.Add(new Transition(typeof(Rolling), Block, Jump, Move));
        animationHash = Animator.StringToHash("Idling");
    }

    public override void AfterExecution()
    {
        anim.SetBool(animationHash, false);
    }

    public override void BeforeExecution()
    {
        Debug.Log("Cooldown");
        timer = 0;
        anim.SetBool(animationHash, true);
    }

    public override void DuringExecution()
    {
        timer += Time.deltaTime;
    }
}
