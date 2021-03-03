using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AgentCombat : MonoBehaviour
{
    public float damage = 10f;

    public StateMachine StateMachine { get; private set; }

    private void Awake()
    {
        StateMachine = new StateMachine();
        Dictionary<Type, State> states = new Dictionary<Type, State>()
        {
            {typeof(ReadyState), new ReadyState(gameObject) },

            {typeof(Blocking), new Blocking(gameObject) },
            {typeof(Stabbing), new Stabbing(gameObject) },
            {typeof(RightSlashing), new RightSlashing(gameObject) },
            {typeof(LeftSlashing), new LeftSlashing(gameObject) },

            {typeof(Drawing), new Drawing(gameObject) },
            {typeof(Loosing), new Loosing(gameObject) },

            {typeof(Equipping), new Equipping(gameObject) },
        };
        StateMachine.SetStates(states, typeof(ReadyState));
    }

    private void Update()
    {
        StateMachine.ExecuteState();
    }
}
