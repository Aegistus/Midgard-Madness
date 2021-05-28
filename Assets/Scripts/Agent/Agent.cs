using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Agent : MonoBehaviour
{
    public AgentStats agentStats;
    public AgentSounds agentSounds;

    public bool Attack { get; set; }
    public bool Block { get; set; }
    public bool Forwards { get; set; }
    public bool Backwards { get; set; }
    public bool Left { get; set; }
    public bool Right { get; set; }
    public bool Jump { get; set; }
    public bool Crouch { get; set; }
    public bool Run { get; set; }
    public bool Dodge { get; set; }
    public bool Equipping { get; set; }
    public int WeaponNumKey { get; set; }
    public bool UnEquipping { get; set; }
    public Ray Aim { get; set; }

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

            {typeof(WalkingForward), new WalkingForward(gameObject) },
            {typeof(WalkingBackward), new WalkingBackward(gameObject) },
            {typeof(StrafingLeft), new StrafingLeft(gameObject) },
            {typeof(StrafingRight), new StrafingRight(gameObject) },

            {typeof(Jumping), new Jumping(gameObject) },
            {typeof(Falling), new Falling(gameObject) },
            {typeof(Running), new Running(gameObject) },

            {typeof(Crouching), new Crouching(gameObject) },
            {typeof(Rolling), new Rolling(gameObject) },

            {typeof(DodgingRight), new DodgingRight(gameObject) },
            {typeof(DodgingLeft), new DodgingLeft(gameObject) },

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
