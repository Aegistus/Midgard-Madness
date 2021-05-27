using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitNode : Node
{
    private Node childNode;
    private float timer = 0;
    private float maxTimer;

    public WaitNode(Node childNode, float maxTimer)
    {
        this.childNode = childNode;
        this.maxTimer = maxTimer;
    }

    public override NodeState Evaluate(float deltaTime)
    {
        timer += deltaTime;
        if (timer >= maxTimer)
        {
            CurrentState = childNode.Evaluate(deltaTime);
            timer = 0;
        }
        else
        {
            CurrentState = NodeState.FAILURE;
        }
        return CurrentState;
    }
}
