using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class AgentController : MonoBehaviour
{
    public AttackDirection AttackDirection { get; set; }
    public bool Attack { get; set; }
    public bool Block { get; set; }
    public bool Forwards { get; set; }
    public bool Backwards { get; set; }
    public bool Left { get; set; }
    public bool Right { get; set; }
    public bool Jump { get; set; }
    public bool Crouch { get; set; }
    public bool Run { get; set; }
    public bool Equipping { get; set; }
    public int WeaponNumKey { get; set; }
}
