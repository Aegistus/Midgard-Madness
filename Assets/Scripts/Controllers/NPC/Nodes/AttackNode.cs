using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackNode : Node
{
    private AgentController controller;

    public AttackNode(AgentController controller)
    {
        this.controller = controller;
    }

    public override NodeState Evaluate()
    {
        Debug.Log("NPC Attacking");
        controller.Attack = true;
        return NodeState.SUCCESS;
    }
}
