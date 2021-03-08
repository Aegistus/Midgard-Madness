using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;

public abstract class NPCState : State
{
    protected SelectorNode rootNode;
    protected NavMeshAgent navAgent;
    protected NPCController controller;
    protected FieldOfView fov;
    protected AgentWeapons weapons;

    public Func<bool> PlayerInSight => () => fov.visibleTargets.Count > 0;

    protected NodeState AtDestination(float maxDistance)
    {
        return Vector3.Distance(transform.position, navAgent.destination) <= maxDistance ? NodeState.SUCCESS : NodeState.FAILURE;
    }

    public NodeState HasWeaponEquipped()
    {
        return weapons.primarySlot.CurrentlyEquipped != null || weapons.secondarySlot.CurrentlyEquipped != null ? NodeState.SUCCESS : NodeState.FAILURE;
    }

    protected NPCState(GameObject gameObject) : base(gameObject)
    {
        navAgent = gameObject.GetComponent<NavMeshAgent>();
        controller = gameObject.GetComponent<NPCController>();
        fov = gameObject.GetComponent<FieldOfView>();
        weapons = gameObject.GetComponent<AgentWeapons>();
        CreateTree();
    }

    protected abstract void CreateTree();

    public override void DuringExecution()
    {
        rootNode.Evaluate();
    }
}
