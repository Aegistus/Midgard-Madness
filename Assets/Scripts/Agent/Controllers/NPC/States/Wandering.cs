using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Wandering : NPCState
{
    private float waitTime = 3f;

    public Wandering(GameObject gameObject) : base(gameObject)
    {
        transitionsTo.Add(new Transition(typeof(Chasing), PlayerInSight));
    }

    public override void AfterExecution()
    {

    }

    public override void BeforeExecution()
    {
        Debug.Log("Wandering");
    }

    protected override void CreateTree()
    {
        ActionNode findNewDestination = new ActionNode(() => controller.SetRandomDestination(false));
        WaitNode waitNode = new WaitNode(findNewDestination, waitTime);
        ActionNode atDestination = new ActionNode(() => AtDestination(1));
        SequenceNode findNewPointSequence = new SequenceNode(new List<Node>() { atDestination, waitNode });

        ActionNode walkToDestination = new ActionNode(() => controller.MoveToDestination(false));

        rootNode = new SelectorNode(new List<Node>() { findNewPointSequence, walkToDestination });
    }
}
