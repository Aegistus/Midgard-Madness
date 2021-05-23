using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blocking : AgentState
{
    private float blockTime = 2f;
    private float timer = 0;

    public Blocking(GameObject gameObject) : base(gameObject)
    {
        transitionsTo.Add(new Transition(typeof(BlockingCooldown), Not(Block)));
        transitionsTo.Add(new Transition(typeof(BlockingCooldown), Not(ShieldEquipped), () => timer >= blockTime));
        transitionsTo.Add(new Transition(typeof(Idling), () => vigor.CurrentVigor < agentStats.blockCost));
        transitionsTo.Add(new Transition(typeof(Rolling), Block, Jump, Move));
    }

    public override void AfterExecution()
    {

    }

    public override void BeforeExecution()
    {
        Debug.Log("Blocking");
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
