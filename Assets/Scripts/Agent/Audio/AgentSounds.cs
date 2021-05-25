using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewAgentSounds", menuName = "Agent Sounds", order = 3)]
public class AgentSounds : ScriptableObject
{
    public SoundGroup breathing;
    public SoundGroup heavyBreathing;
    public SoundGroup footsteps;
    public SoundGroup attack;
    public SoundGroup hit;
    public SoundGroup death;
}
