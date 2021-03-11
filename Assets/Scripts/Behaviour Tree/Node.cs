using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class Node
{
    public delegate NodeState NodeReturn();

    public NodeState CurrentState { get; protected set; }

    public abstract NodeState Evaluate(float deltaTime);

    public static bool ConvertToBool(NodeState state)
    {
        switch (state)
        {
            case NodeState.SUCCESS: return true;
            case NodeState.FAILURE: return false;
            case NodeState.RUNNING: return true;
            default: return true;
        }
    }

    public static NodeState ConvertToState(bool condition)
    {
        if (condition == true)
        {
            return NodeState.SUCCESS;
        }
        else
        {
            return NodeState.FAILURE;
        }
    }
}
