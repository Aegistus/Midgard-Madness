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
    public Transform Target { get; set; }
    public Vector3 Destination => navAgent.destination;

    public StateMachine AIStateMachine { get; private set; }
    public NPCState CurrentState => (NPCState)AIStateMachine.CurrentState;

    private void Awake()
    {
        weapons = GetComponent<AgentWeapons>();
        navAgent = GetComponent<NavMeshAgent>();
        Dictionary<Type, State> states = new Dictionary<Type, State>()
        {
            {typeof(AIWandering), new AIWandering(gameObject) },
            {typeof(AISearching), new AISearching(gameObject) },
            {typeof(AIFighting), new AIFighting(gameObject) },
            {typeof(AIChasing), new AIChasing(gameObject) },
        };
        AIStateMachine = new StateMachine();
        AIStateMachine.SetStates(states, typeof(AIWandering));
        StartCoroutine(RunAIStateMachine());
    }

    public void SetDestination(Vector3 position, bool running)
    {
        navAgent.SetDestination(position);
        Forwards = true;
        if (running)
        {
            Run = true;
        }
    }

    public bool AtDestination(float maxDistance)
    {
        return Vector3.Distance(transform.position, Destination) <= maxDistance;
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

    public void EquipWeapon(int weaponChoice)
    {
        if (weaponChoice >= 0 && weaponChoice < weapons.CarriedWeapons.Count)
        {
            Equipping = true;
            WeaponNumKey = weaponChoice;
        }
    }

    public NodeState UnEquipAll()
    {
        weapons.EquipUnarmed();
        return NodeState.SUCCESS;
    }

    public NodeState AttackEnemy()
    {
        if (Attack != true)
        {
            Attack = true;
        }
        return NodeState.SUCCESS;
    }

    public NodeState MomentumAttackEnemy()
    {
        Forwards = true;
        Run = true;
        if (Attack != true)
        {
            Attack = true;
        }
        return NodeState.SUCCESS;
    }

    public void SetRandomDestination(bool running)
    {
        Debug.Log("Finding Patrol Point");
        Vector3 randomPoint = new Vector3((Random.value * wanderDiameter) - (wanderDiameter/2), 0, (Random.value * wanderDiameter) - (wanderDiameter / 2));
        randomPoint += transform.position;
        SetDestination(randomPoint, running);
    }

    public void MoveToDestination(bool running)
    {
        Forwards = true;
        if (running)
        {
            Run = true;
        }
    }

    public NodeState NearTarget(float distance)
    {
        if (Vector3.Distance(transform.position, Target.position) <= distance)
        {
            return NodeState.SUCCESS;
        }
        else
        {
            return NodeState.FAILURE;
        }
    }

    public void OnDrawGizmos()
    {
        if (Application.isPlaying)
        {
            Gizmos.color = Color.red;
            Vector3[] path = navAgent.path.corners;
            foreach (var point in path)
            {
                Gizmos.DrawSphere(point, .1f);
            }
        }
    }
}
