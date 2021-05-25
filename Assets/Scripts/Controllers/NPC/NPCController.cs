using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;
using Random = UnityEngine.Random;

public class NPCController : AgentController
{
    public float tickInterval = .1f;
    public LayerMask playerLayer;

    public float wanderDiameter = 5f;
    public float wanderWaitTime = 5f;
    public float attackRadius = 2f;
    public float attackWaitTime = 2f;

    private Agent agent;
    private AgentMovement movement;
    private NavMeshAgent navAgent;
    private FieldOfView fov;
    public Transform Target { get; private set; }
    public Vector3 TargetLastPosition { get; private set; }
    public Vector3 Destination => navAgent.destination;

    public StateMachine AIStateMachine { get; private set; }
    public NPCState CurrentState => (NPCState)AIStateMachine.CurrentState;

    private void Awake()
    {
        agent = GetComponent<Agent>();
        movement = GetComponent<AgentMovement>();
        navAgent = GetComponent<NavMeshAgent>();
        fov = GetComponent<FieldOfView>();
        Dictionary<Type, State> states = new Dictionary<Type, State>()
        {
            {typeof(AIWandering), new AIWandering(gameObject) },
            {typeof(AISearching), new AISearching(gameObject) },
            {typeof(AIFighting), new AIFighting(gameObject) },
            {typeof(AIChasing), new AIChasing(gameObject) },
            {typeof(AIEquipping), new AIEquipping(gameObject) },
        };
        AIStateMachine = new StateMachine();
        AIStateMachine.SetStates(states, typeof(AIWandering));
        StartCoroutine(RunAIStateMachine());
    }

    RaycastHit rayHit;
    private void Update()
    {
        if (fov.visibleTargets.Count > 0)
        {
            Target = fov.visibleTargets[0];
        }
        else if (Physics.SphereCast(transform.position, 3f, Vector3.one, out rayHit, 1, playerLayer))
        {
            Target = rayHit.transform;
        }
        else
        {
            Target = null;
        }
        if (Target != null)
        {
            TargetLastPosition = Target.position;
        }
    }

    public void SetDestination(Vector3 position, bool running)
    {
        if (navAgent.isOnNavMesh)
        {
            navAgent?.SetDestination(position);
        }
        agent.Forwards = true;
        if (running)
        {
            agent.Run = true;
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
            agent.Attack = false;
            agent.Block = false;
            agent.Forwards = false;
            agent.Backwards = false;
            agent.Left = false;
            agent.Right = false;
            agent.Jump = false;
            agent.Crouch = false;
            agent.Run = false;
            agent.Equipping = false;
            if (Target != null)
            {
                Vector3 randomOffset = Target.position + new Vector3(Random.Range(-.5f, .5f), Random.Range(-.5f, .5f), Random.Range(-.5f, .5f));
                agent.Aim = new Ray(transform.position, randomOffset - transform.position);
            }
            AIStateMachine.ExecuteState();
            yield return new WaitForSeconds(tickInterval);
        }
    }

    public void LookAt(Transform target)
    {
        movement.lookDirection.LookAt(target);
    }

    public void SetRandomDestination(bool running)
    {
        Debug.Log("Finding Patrol Point");
        Vector3 randomPoint = new Vector3((Random.value * wanderDiameter) - (wanderDiameter/2), 0, (Random.value * wanderDiameter) - (wanderDiameter / 2));
        randomPoint += transform.position;
        SetDestination(randomPoint, running);
    }

    public bool NearTarget(float distance)
    {
        if (Vector3.Distance(transform.position, Target.position) <= distance)
        {
            return true;
        }
        else
        {
            return false;
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
