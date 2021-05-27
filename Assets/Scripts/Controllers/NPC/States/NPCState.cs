using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class NPCState : State
{
    protected SelectorNode rootNode;
    protected NPCController controller;
    protected Agent agent;
    protected AgentWeapons weapons;

    public Func<bool> PlayerInSight => () => controller.Target != null;

    protected NPCState(GameObject gameObject) : base(gameObject)
    {
        controller = gameObject.GetComponent<NPCController>();
        agent = gameObject.GetComponent<Agent>();
        weapons = gameObject.GetComponent<AgentWeapons>();
        CreateTree();
    }

    protected abstract void CreateTree();

    public override void DuringExecution()
    {
        if (rootNode != null)
        {
            rootNode.Evaluate(controller.tickInterval);
        }
    }
}
