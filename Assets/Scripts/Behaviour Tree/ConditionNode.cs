using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConditionNode : Node
{
    public delegate NodeState ConditionNodeDelegate();

    private ConditionNodeDelegate condition;

    public ConditionNode(ConditionNodeDelegate condition)
    {
        this.condition = condition;
    }

    public override NodeState Evaluate(float deltaTime)
    {
        CurrentState = condition();
        return CurrentState;
    }
}
