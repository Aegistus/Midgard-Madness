using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionNode : Node
{
    public delegate NodeState ActionNodeDelegate();

    private ActionNodeDelegate action;

    public ActionNode(ActionNodeDelegate action)
    {
        this.action = action;
    }

    public override NodeState Evaluate()
    {
        switch (action())
        {
            case NodeState.SUCCESS:
                CurrentState = NodeState.SUCCESS;
                return NodeState.SUCCESS;
            case NodeState.RUNNING:
                CurrentState = NodeState.RUNNING;
                return NodeState.RUNNING;
            case NodeState.FAILURE:
                CurrentState = NodeState.FAILURE;
                return NodeState.FAILURE;
            default:
                CurrentState = NodeState.FAILURE;
                return NodeState.SUCCESS;
        }
    }
}
