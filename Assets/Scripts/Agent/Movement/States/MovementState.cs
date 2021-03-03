using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MovementState : State
{
    protected Animator anim;
    protected int animationHash;
    protected int fullBodyLayer;

    protected LayerMask groundLayer;
    protected AgentMovement movement;
    protected AgentController controller;
    protected AgentCombat combat;
    protected CharacterController charController;
    protected List<string> soundNames = new List<string>();

    public Func<bool> Move => () => controller.Forwards || controller.Backwards || controller.Right || controller.Left;
    public Func<bool> Jump => () => controller.Jump;
    public Func<bool> Run => () => controller.Run;
    public Func<bool> Crouch => () => controller.Crouch;
    public Func<bool> Roll => () => controller.Roll;
    public Func<bool> OnGround => () => movement.IsGrounded();
    public Func<bool> NextToWall => () => IsNextToWall();
    public Func<bool> LedgeInReach => () => movement.ledgeDetector.CollidingWith == 0;
    public Func<bool> FacingHighWall => () => movement.wallDetectorUpper.CollidingWith > 0;
    public Func<bool> FacingLowWall => () => movement.wallDetectorLower.CollidingWith > 0;
    public Func<bool> OtherSideOfVaultOpen => () => movement.vaultOtherSideDetector.CollidingWith == 0;
    public Func<bool> Rising => () => charController.velocity.y > .01f;
    public Func<bool> Falling => () => charController.velocity.y < -.1f;
    public Func<bool> Attacking => () => combat.StateMachine.CurrentState is AttackingState;

    public MovementState(GameObject gameObject) : base(gameObject)
    {
        movement = gameObject.GetComponent<AgentMovement>();
        controller = gameObject.GetComponent<AgentController>();
        groundLayer = movement.groundLayer;
        charController = gameObject.GetComponent<CharacterController>();
        combat = gameObject.GetComponent<AgentCombat>();
        anim = gameObject.GetComponentInChildren<Animator>();
        fullBodyLayer = anim.GetLayerIndex("Full Body");
    }

    private bool IsNextToWall()
    {
        if (Physics.Raycast(new Ray(transform.position, transform.right), 1f, groundLayer))
        {
            return true;
        }
        if (Physics.Raycast(new Ray(transform.position, -transform.right), 1f, groundLayer))
        {
            return true;
        }
        return false;
    }

    Quaternion targetRotation, currentRotation;
    protected void RotateAgentModelToDirection(Vector3 position, float rotationSpeed = 15f)
    {
        // make the agent's model rotate towards the direction
        currentRotation = movement.agentModel.rotation;
        movement.agentModel.LookAt(movement.agentModel.position + position);
        targetRotation.eulerAngles = new Vector3(0, movement.agentModel.eulerAngles.y, 0);
        movement.agentModel.rotation = currentRotation;
        movement.agentModel.rotation = Quaternion.Lerp(currentRotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    Vector3 newVelocity;
    protected Vector3 GetAgentMovementInput()
    {
        newVelocity = Vector3.zero;
        if (controller.Forwards)
        {
            newVelocity += movement.lookDirection.forward;
        }
        if (controller.Backwards)
        {
            newVelocity += -movement.lookDirection.forward;
        }
        if (controller.Left)
        {
            newVelocity += -movement.lookDirection.right;
        }
        if (controller.Right)
        {
            newVelocity += movement.lookDirection.right;
        }
        newVelocity = newVelocity.normalized;
        return newVelocity;
    }

}
