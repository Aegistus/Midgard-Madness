using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectorNode : Node
{
    protected List<Node> childNodes = new List<Node>();

    public SelectorNode(List<Node> childNodes)
    {
        this.childNodes = childNodes;
    }

    public override NodeState Evaluate()
    {
        for (int i = 0; i < childNodes.Count; i++)
        {
            switch (childNodes[i].Evaluate())
            {
                case NodeState.FAILURE:
                    continue;
                case NodeState.RUNNING:
                    CurrentState = NodeState.RUNNING;
                    return CurrentState;
                case NodeState.SUCCESS:
                    CurrentState = NodeState.SUCCESS;
                    return CurrentState;
            }
        }
        CurrentState = NodeState.FAILURE;
        return CurrentState;
    }
}
