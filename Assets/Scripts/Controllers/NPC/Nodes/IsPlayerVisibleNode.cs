using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsPlayerVisibleNode : Node
{
    private FieldOfView fov;
    
    public IsPlayerVisibleNode(FieldOfView fov)
    {
        this.fov = fov;
    }

    public override NodeState Evaluate()
    {
        Debug.Log("NPC Checking Player Visibility");
        return fov.visibleTargets.Count > 0 ? NodeState.SUCCESS : NodeState.FAILURE;
    }
}
