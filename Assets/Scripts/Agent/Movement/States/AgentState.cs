﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AgentState : State
{
    protected Animator anim;
    protected int animationHash;
    protected int fullBodyLayer;

    protected LayerMask groundLayer;
    protected AgentMovement movement;
    protected AgentController controller;
    protected AgentCombat combat;
    protected AgentWeapons weapons;
    protected CharacterController charController;
    protected List<string> soundNames = new List<string>();

    public Func<bool> Move => () => controller.Forwards || controller.Backwards || controller.Right || controller.Left;
    public Func<bool> Jump => () => controller.Jump;
    public Func<bool> Run => () => controller.Run;
    public Func<bool> Crouch => () => controller.Crouch;
    public Func<bool> OnGround => () => movement.IsGrounded();
    public Func<bool> NextToWall => () => IsNextToWall();
    public Func<bool> Rising => () => charController.velocity.y > .01f;
    public Func<bool> Falling => () => charController.velocity.y < -.1f;
    public Func<bool> Attack => () => controller.Attack;
    public Func<bool> Block => () => controller.Block;
    public Func<bool> EquipWeaponInput => () => controller.Equipping;
    public Func<bool> MeleeEquipped => () => weapons.primarySlot.CurrentlyEquipped?.GetType() == typeof(MeleeWeapon);
    public Func<bool> RangedEquipped => () => weapons.primarySlot.CurrentlyEquipped?.GetType() == typeof(RangedWeapon) || weapons.secondarySlot.CurrentlyEquipped?.GetType() == typeof(RangedWeapon);

    public AgentState(GameObject gameObject) : base(gameObject)
    {
        movement = gameObject.GetComponent<AgentMovement>();
        controller = gameObject.GetComponent<AgentController>();
        groundLayer = movement.groundLayer;
        charController = gameObject.GetComponent<CharacterController>();
        combat = gameObject.GetComponent<AgentCombat>();
        weapons = gameObject.GetComponent<AgentWeapons>();
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