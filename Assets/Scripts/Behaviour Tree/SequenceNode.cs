using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SequenceNode : Node
{
    private List<Node> childNodes = new List<Node>();

    public SequenceNode(List<Node> childNodes)
    {
        this.childNodes = childNodes;
    }

    public override NodeState Evaluate()
    {
        bool anyChildRunning = false;
        for (int i = 0; i < childNodes.Count; i++)
        {
            switch (childNodes[i].Evaluate())
            {
                case NodeState.FAILURE:
                    CurrentState = NodeState.FAILURE;
                    return CurrentState;
                case NodeState.RUNNING:
                    anyChildRunning = true;
                    continue;
                case NodeState.SUCCESS:
                    continue;
            }
        }
        CurrentState = anyChildRunning ? NodeState.RUNNING : NodeState.SUCCESS;
        return CurrentState;
    }
}
