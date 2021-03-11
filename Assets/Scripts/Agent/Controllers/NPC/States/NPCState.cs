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
    protected AgentWeapons weapons;

    public Func<bool> PlayerInSight => () => fov.visibleTargets.Count > 0;

    protected NodeState AtDestination(float maxDistance)
    {
        return Vector3.Distance(transform.position, controller.Destination) <= maxDistance ? NodeState.SUCCESS : NodeState.FAILURE;
    }

    public NodeState HasWeaponEquipped()
    {
        return weapons.primarySlot.CurrentlyEquipped != null || weapons.secondarySlot.CurrentlyEquipped != null ? NodeState.SUCCESS : NodeState.FAILURE;
    }

    public NodeState IsRunning()
    {
        return controller.Run ? NodeState.SUCCESS : NodeState.FAILURE;
    }

    protected NPCState(GameObject gameObject) : base(gameObject)
    {
        controller = gameObject.GetComponent<NPCController>();
        fov = gameObject.GetComponent<FieldOfView>();
        weapons = gameObject.GetComponent<AgentWeapons>();
        CreateTree();
    }

    protected abstract void CreateTree();

    public override void DuringExecution()
    {
        rootNode.Evaluate(controller.tickInterval);
    }
}
