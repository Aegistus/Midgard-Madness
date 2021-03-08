using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class Node
{
    public delegate NodeState NodeReturn();

    public NodeState CurrentState { get; protected set; }

    public abstract NodeState Evaluate(float deltaTime);
}
