using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AIFighting : NPCState
{
    Func<bool> NotAttacking => Not(() => agent.CurrentState.GetType() != typeof(MeleeAttacking) && agent.CurrentState.GetType() != typeof(RangedAttacking));

    public AIFighting(GameObject gameObject) : base(gameObject)
    {
        transitionsTo.Add(new Transition(typeof(AISearching), NotAttacking, Not(PlayerInSight)));
        transitionsTo.Add(new Transition(typeof(AIChasing), NotAttacking, PlayerInSight, Not(() => controller.NearTarget(controller.attackRadius))));
    }

    public override void AfterExecution()
    {

    }

    public override void BeforeExecution()
    {
        Debug.Log("NPC Fighting");
        controller.SetDestination(transform.position);
        if (UnityEngine.Random.value > .5) // 50/50 chance
        {
            agent.Forwards = true;
            agent.Attack = true;
        }
    }

    public override void DuringExecution()
    {
        base.DuringExecution();
        controller.LookAt(controller.Target);
    }

    protected override void CreateTree()
    {
        rootNode = new SelectorNode(new List<Node>()
        {
            // attack sequence
            new SequenceNode(new List<Node>()
            {
                new WaitNode(new ActionNode(() => agent.Attack = true), UnityEngine.Random.value * controller.attackWaitTime)
            }),
            // block sequence
            new SequenceNode(new List<Node>()
            {
                new WaitNode(new ActionNode(() => agent.Block = true), UnityEngine.Random.value * controller.attackWaitTime)
            }),
        });
    }

}
