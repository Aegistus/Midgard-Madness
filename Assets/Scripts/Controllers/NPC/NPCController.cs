using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;
using Random = UnityEngine.Random;

public class NPCController : AgentController
{
    public float wanderDiameter = 5f;
    public float wanderWaitTime = 5f;

    private FieldOfView fov;
    private AgentWeapons weapons;
    private AgentMovement movement;
    private AgentCombat combat;
    private NavMeshAgent navAgent;

    private StateMachine stateMachine;


    private void Awake()
    {
        fov = GetComponent<FieldOfView>();
        weapons = GetComponent<AgentWeapons>();
        movement = GetComponent<AgentMovement>();
        combat = GetComponent<AgentCombat>();
        navAgent = GetComponent<NavMeshAgent>();
        Dictionary<Type, State> states = new Dictionary<Type, State>()
        {
            {typeof(Wandering), new Wandering(gameObject) },
            {typeof(Searching), new Searching(gameObject) },
            {typeof(Fighting), new Fighting(gameObject) },
        };
        stateMachine = new StateMachine();
        stateMachine.SetStates(states, typeof(Wandering));
    }

    public NodeState SetDestination(Vector3 position)
    {
        navAgent.SetDestination(position);
        Forwards = true;
        return NodeState.SUCCESS;
    }

    private void Update()
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
        stateMachine.ExecuteState();
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

    public NodeState SetRandomDestination()
    {
        Debug.Log("Finding Patrol Point");
        Vector3 randomPoint = new Vector3((Random.value * wanderDiameter) - (wanderDiameter/2), 0, (Random.value * wanderDiameter) - (wanderDiameter / 2));
        randomPoint += transform.position;
        return SetDestination(randomPoint);
    }

    public NodeState WalkToDestination()
    {
        Forwards = true;
        return NodeState.SUCCESS;
    }
}
