using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blocking : AgentState
{
    private float blockTime = .5f;
    private float timer = 0;

    public Blocking(GameObject gameObject) : base(gameObject)
    {
        animationHash = Animator.StringToHash("Blocking");
        transitionsTo.Add(new Transition(typeof(BlockingCooldown), Not(Block)));
        transitionsTo.Add(new Transition(typeof(BlockingCooldown), Not(ShieldEquipped), () => timer >= blockTime));
        transitionsTo.Add(new Transition(typeof(Idling), () => vigor.CurrentVigor < agentStats.blockCost));
    }

    public override void AfterExecution()
    {
        anim.SetBool(animationHash, false);
    }

    public override void BeforeExecution()
    {
        Debug.Log("Blocking");
        anim.SetBool(animationHash, true);
        self.SetHorizontalVelocity(Vector3.zero);
        timer = 0;
    }

    public override void DuringExecution()
    {
        timer += Time.deltaTime;
        self.RotateAgentModelToDirection(self.lookDirection.forward);
        vigor.DepleteVigor(agentStats.blockCost * Time.deltaTime);
    }
}
