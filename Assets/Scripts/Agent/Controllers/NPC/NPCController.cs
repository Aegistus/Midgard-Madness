using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;
using Random = UnityEngine.Random;

public class NPCController : AgentController
{
    public float tickInterval = .1f;

    public float wanderDiameter = 5f;
    public float wanderWaitTime = 5f;
    public float attackRadius = 2f;

    private AgentWeapons weapons;
    private NavMeshAgent navAgent;

    public StateMachine AIStateMachine { get; private set; }
    public NPCState CurrentState => (NPCState)AIStateMachine.CurrentState;

    private void Awake()
    {
        weapons = GetComponent<AgentWeapons>();
        navAgent = GetComponent<NavMeshAgent>();
        Dictionary<Type, State> states = new Dictionary<Type, State>()
        {
            {typeof(Wandering), new Wandering(gameObject) },
            {typeof(Searching), new Searching(gameObject) },
            {typeof(Fighting), new Fighting(gameObject) },
            {typeof(Chasing), new Chasing(gameObject) },
        };
        AIStateMachine = new StateMachine();
        AIStateMachine.SetStates(states, typeof(Wandering));
        StartCoroutine(RunAIStateMachine());
    }

    public NodeState SetDestination(Vector3 position, bool running)
    {
        navAgent.SetDestination(position);
        Forwards = true;
        if (running)
        {
            Run = true;
        }
        return NodeState.SUCCESS;
    }

    private IEnumerator RunAIStateMachine()
    {
        while (true)
        {
            Attack = false;
            Block = false;
            Forwards = false;
            Backwards = false;
            Left = false;
            Right = false;
            Jump = false;
            Crouch = false;
            Run = false;
            Equipping = false;
            AIStateMachine.ExecuteState();
            yield return new WaitForSeconds(tickInterval);
        }
    }

    public NodeState EquipWeapon()
    {
        if (weapons.primarySlot.CurrentlyEquipped != null || weapons.secondarySlot.CurrentlyEquipped != null)
        {
            return NodeState.SUCCESS;
        }
        else
        {
            Equipping = true;
            WeaponNumKey = 1;
            return weapons.primarySlot.CurrentlyEquipped != null || weapons.secondarySlot.CurrentlyEquipped != null ? NodeState.SUCCESS : NodeState.FAILURE;
        }
    }

    public NodeState AttackEnemy()
    {
        if (Attack != true)
        {
            Attack = true;
        }
        return NodeState.SUCCESS;
    }

    public NodeState SetRandomDestination(bool running)
    {
        Debug.Log("Finding Patrol Point");
        Vector3 randomPoint = new Vector3((Random.value * wanderDiameter) - (wanderDiameter/2), 0, (Random.value * wanderDiameter) - (wanderDiameter / 2));
        randomPoint += transform.position;
        return SetDestination(randomPoint, running);
    }

    public NodeState MoveToDestination(bool running)
    {
        Forwards = true;
        if (running)
        {
            Run = true;
        }
        return NodeState.SUCCESS;
    }
}
