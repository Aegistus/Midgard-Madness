using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AIWandering : NPCState
{
    public AIWandering(GameObject gameObject) : base(gameObject)
    {
        transitionsTo.Add(new Transition(typeof(AIChasing), PlayerInSight));
    }

    public override void AfterExecution()
    {

    }

    public override void BeforeExecution()
    {
        Debug.Log("NPC Wandering");
        controller.SetDestination(transform.position, false);
        controller.SetRandomDestination(false);
        agent.UnEquipping = true;
    }

    protected override void CreateTree()
    {
        rootNode = new SelectorNode(new List<Node>()
        {
            // find new point sequence
            new SequenceNode(new List<Node>()
            {
                // idle for a few seconds if at destination
                new WaitNode(new ConditionNode(() => Node.ConvertToState(controller.AtDestination(1))), controller.wanderWaitTime),
                new ActionNode(() => controller.SetRandomDestination(false)),
            }),
            // walk to destination sequence
            new SequenceNode(new List<Node>()
            {
                new ActionNode(() => agent.Forwards = true),
            })
        });
    }
}
