﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AISearching : NPCState
{
    private float timer = 0;
    private float maxSearchTimer = 10f;

    public AISearching(GameObject gameObject) : base(gameObject)
    {
        transitionsTo.Add(new Transition(typeof(AIChasing), PlayerInSight));
        transitionsTo.Add(new Transition(typeof(AIWandering), () => timer >= maxSearchTimer));
        transitionsTo.Add(new Transition(typeof(AIWandering), () => controller.AtDestination(1f)));
    }

    public override void AfterExecution()
    {

    }

    public override void BeforeExecution()
    {
        Debug.Log("NPC Searching");
        timer = 0f;
        controller.SetDestination(controller.TargetLastPosition);
    }

    public override void DuringExecution()
    {
        base.DuringExecution();
        agent.Forwards = true;
        controller.LookAtNextWaypoint();
        timer += Time.deltaTime;
    }

    protected override void CreateTree()
    {
        
    }
}
