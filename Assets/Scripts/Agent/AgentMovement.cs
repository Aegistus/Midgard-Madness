using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AgentMovement : MonoBehaviour
{
    public LayerMask groundLayer;
    public Transform lookDirection;
    public Transform agentModel;
    public float groundCheckRadius = .75f;
    public float groundCheckHeight = 0;

    public Vector3 Velocity { get { return velocity; } private set { velocity = value; } }
    private Vector3 velocity;

    public bool Grounded { get; private set; }

    private CharacterController charController;
    private NavMeshAgent navAgent;
    private float verticalVelocity;

    private void Awake()
    {
        charController = GetComponent<CharacterController>();
        navAgent = GetComponent<NavMeshAgent>();
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
        if (Physics.CheckSphere(new Vector3(transform.position.x, transform.position.y + groundCheckHeight, transform.position.z), groundCheckRadius, groundLayer))
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
