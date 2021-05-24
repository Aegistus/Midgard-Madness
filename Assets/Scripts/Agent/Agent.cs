using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Agent : MonoBehaviour
{
    public AgentStats agentStats;
    public AgentSounds agentSounds;

    public AgentState CurrentState => (AgentState)StateMachine.CurrentState;

    public StateMachine StateMachine { get; private set; }

    private void Awake()
    {
        StateMachine = new StateMachine();
    }

    private void Start()
    {
        Dictionary<Type, State> states = new Dictionary<Type, State>()
        {
            {typeof(Idling), new Idling(gameObject) },
            {typeof(Walking), new Walking(gameObject) },
            {typeof(Jumping), new Jumping(gameObject) },
            {typeof(Falling), new Falling(gameObject) },
            {typeof(Running), new Running(gameObject) },
            {typeof(Crouching), new Crouching(gameObject) },
            {typeof(Rolling), new Rolling(gameObject) },

            {typeof(Blocking), new Blocking(gameObject) },
            {typeof(BlockingCooldown), new BlockingCooldown(gameObject) },
            {typeof(MeleeAttacking), new MeleeAttacking(gameObject) },
            {typeof(MomentumAttacking), new MomentumAttacking(gameObject) },

            {typeof(RangedAiming), new RangedAiming(gameObject) },
            {typeof(RangedAttacking), new RangedAttacking(gameObject) },

            {typeof(Equipping), new Equipping(gameObject) },
            {typeof(UnEquipping), new UnEquipping(gameObject) },

            {typeof(TakingDamage), new TakingDamage(gameObject) },
            {typeof(Dying), new Dying(gameObject) },
        };
        StateMachine.SetStates(states, typeof(Idling));
    }

    private void Update()
    {
        StateMachine.ExecuteState();
    }

}
