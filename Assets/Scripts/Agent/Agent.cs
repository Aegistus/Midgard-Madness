using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Agent : MonoBehaviour
{
    public AgentStats agentStats;
    public AgentSounds agentSounds;
    public LayerMask groundLayer;
    public Transform lookDirection;
    public Transform agentModel;

    public Vector3 Velocity { get { return velocity; } private set { velocity = value; } }
    private Vector3 velocity;

    public AgentState CurrentState => (AgentState)StateMachine.CurrentState;

    public StateMachine StateMachine { get; private set; }
    private CharacterController charController;
    private NavMeshAgent navAgent;
    private float verticalVelocity;

    private void Start()
    {
        StateMachine = new StateMachine();
        charController = GetComponent<CharacterController>();
        navAgent = GetComponent<NavMeshAgent>();
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

    public void SetHorizontalVelocity(Vector3 velocity)
    {
        Velocity = new Vector3(velocity.x, Velocity.y, velocity.z);
    }

    public void AddVerticalVelocity(float vertVelocity)
    {
        verticalVelocity += vertVelocity;
    }

    public void SetVerticalVelocity(float vertVelocity)
    {
        verticalVelocity = vertVelocity;
    }

    public bool IsGrounded()
    {
        if (Physics.BoxCast(transform.position, Vector3.one / 15, Vector3.down, transform.rotation, .5f, groundLayer))
        {
            return true;
        }
        return false;
    }

    Quaternion targetRotation, currentRotation;
    public void RotateAgentModelToDirection(Vector3 position, float rotationSpeed = 15f)
    {
        // make the agent's model rotate towards the direction
        currentRotation = agentModel.rotation;
        agentModel.LookAt(agentModel.position + position);
        targetRotation.eulerAngles = new Vector3(0, agentModel.eulerAngles.y, 0);
        agentModel.rotation = currentRotation;
        agentModel.rotation = Quaternion.Lerp(currentRotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    private void Update()
    {
        if (!IsGrounded())
        {
            verticalVelocity += -9.8f * Time.deltaTime;
        }
        else
        {
            verticalVelocity = 0;
        }
        StateMachine.ExecuteState();
        velocity.y = verticalVelocity;
        if (charController.enabled)
        {
            if (navAgent == null || navAgent.enabled == false)
            {
                charController.Move(Velocity * Time.deltaTime);
            }
        }
    }

}
