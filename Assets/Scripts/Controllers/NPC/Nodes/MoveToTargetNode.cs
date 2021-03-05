using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MoveToTargetNode : Node
{
    private Transform transform;
    private AgentController controller;
    private NavMeshAgent navAgent;
    private FieldOfView fov;
    private Transform target;

    public MoveToTargetNode(Transform transform, AgentController controller, NavMeshAgent navAgent, FieldOfView fov)
    {
        this.transform = transform;
        this.controller = controller;
        this.navAgent = navAgent;
        this.fov = fov;
    }

    public override NodeState Evaluate()
    {
        Debug.Log("NPC Moving To Target");
        if (fov.visibleTargets.Count > 0)
        {
            target = fov.visibleTargets[0];
            if (Vector3.Distance(transform.position, target.position) > 2)
            {
                navAgent.SetDestination(target.position);
                controller.Forwards = true;
                return NodeState.RUNNING;
            }
            else
            {
                return NodeState.SUCCESS;
            }
        }
        return NodeState.FAILURE;
    }
}
