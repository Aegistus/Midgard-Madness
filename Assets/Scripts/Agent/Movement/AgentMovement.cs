using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentMovement : MonoBehaviour
{
    public LayerMask groundLayer;
    public Transform lookDirection;
    public Transform agentModel;
    public WallDetector wallDetectorUpper;
    public WallDetector wallDetectorLower;
    public LedgeDetector ledgeDetector;
    public WallDetector vaultOtherSideDetector;

    [HideInInspector]
    public Vector3 velocity;

    public MovementState CurrentState => (MovementState)StateMachine.CurrentState;

    public StateMachine StateMachine { get; private set; }
    private CharacterController charController;
    private float verticalVelocity;

    private void Awake()
    {
        StateMachine = new StateMachine();
        charController = GetComponent<CharacterController>();
        Dictionary<Type, State> states = new Dictionary<Type, State>()
        {
            {typeof(Idling), new Idling(gameObject) },
            {typeof(Walking), new Walking(gameObject) },
            {typeof(Jumping), new Jumping(gameObject) },
            {typeof(Falling), new Falling(gameObject) },
            {typeof(Running), new Running(gameObject) },
            {typeof(Crouching), new Crouching(gameObject) },
            {typeof(Climbing), new Climbing(gameObject) },
            {typeof(Vaulting), new Vaulting(gameObject) },
            {typeof(Sliding), new Sliding(gameObject) },
            {typeof(Rolling), new Rolling(gameObject) },
        };
        StateMachine.SetStates(states, typeof(Idling));
    }

    public void SetHorizontalVelocity(Vector3 velocity)
    {
        this.velocity = new Vector3(velocity.x, this.velocity.y, velocity.z);
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
        if (Physics.BoxCast(transform.position, Vector3.one / 10, Vector3.down, transform.rotation, .5f, groundLayer))
        {
            return true;
        }
        return false;
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
            charController.Move(velocity * Time.deltaTime);
        }
    }
}
