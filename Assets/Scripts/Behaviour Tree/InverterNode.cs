using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InverterNode : Node
{
    private Node childNode;

    public InverterNode(Node childNode)
    {
        this.childNode = childNode;
    }

    public override NodeState Evaluate(float deltaTime)
    {
        switch (childNode.Evaluate(deltaTime))
        {
            case NodeState.SUCCESS:
                CurrentState = NodeState.FAILURE;
                return CurrentState;
            case NodeState.FAILURE:
                CurrentState = NodeState.SUCCESS;
                return CurrentState;
            case NodeState.RUNNING:
                CurrentState = NodeState.RUNNING;
                return CurrentState;
            default:
                return NodeState.SUCCESS;
        }
    }
}
