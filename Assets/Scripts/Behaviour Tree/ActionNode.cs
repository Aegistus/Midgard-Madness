using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionNode : Node
{
    public delegate void ActionNodeDelegate();

    private ActionNodeDelegate action;

    public ActionNode(ActionNodeDelegate action)
    {
        this.action = action;
    }

    public override NodeState Evaluate(float deltaTime)
    {
        action();
        CurrentState = NodeState.SUCCESS;
        return CurrentState;
    }
}
