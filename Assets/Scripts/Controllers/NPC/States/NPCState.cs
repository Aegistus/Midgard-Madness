using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;

public abstract class NPCState : State
{
    protected SelectorNode rootNode;
    protected NPCController controller;
    protected FieldOfView fov;
    protected Agent agent;
    protected AgentWeapons weapons;

    public Func<bool> PlayerInSight => () => fov.visibleTargets.Count > 0;

    protected NPCState(GameObject gameObject) : base(gameObject)
    {
        controller = gameObject.GetComponent<NPCController>();
        fov = gameObject.GetComponent<FieldOfView>();
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
