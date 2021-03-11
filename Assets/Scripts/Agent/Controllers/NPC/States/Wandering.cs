using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Wandering : NPCState
{
    public Wandering(GameObject gameObject) : base(gameObject)
    {
        transitionsTo.Add(new Transition(typeof(Chasing), PlayerInSight));
    }

    public override void AfterExecution()
    {

    }

    public override void BeforeExecution()
    {
        Debug.Log("NPC Wandering");
        controller.SetDestination(transform.position, false);
        controller.SetRandomDestination(false);
    }

    protected override void CreateTree()
    {
        ActionNode findNewDestination = new ActionNode(() => controller.SetRandomDestination(false));
        WaitNode waitNode = new WaitNode(findNewDestination, controller.wanderWaitTime);
        ActionNode atDestination = new ActionNode(() => AtDestination(1));
        SequenceNode findNewPointSequence = new SequenceNode(new List<Node>() { atDestination, waitNode, findNewDestination });

        InverterNode notAtDestination = new InverterNode(new ActionNode(() => AtDestination(1)));
        ActionNode walkToDestination = new ActionNode(() => controller.MoveToDestination(false));
        SequenceNode walkingSequence = new SequenceNode(new List<Node> { notAtDestination, walkToDestination });

        rootNode = new SelectorNode(new List<Node>() { findNewPointSequence, walkToDestination });
    }
}
