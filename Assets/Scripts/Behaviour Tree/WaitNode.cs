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

    public override NodeState Evaluate()
    {
        timer += Time.deltaTime;
        if (timer >= maxTimer)
        {
            CurrentState = childNode.Evaluate();
            timer = 0;
        }
        else
        {
            CurrentState = NodeState.RUNNING;
        }
        return CurrentState;
    }
}
