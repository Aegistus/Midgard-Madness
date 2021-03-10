using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class AgentState : State
{
    protected Animator anim;
    protected int animationHash;

    protected LayerMask groundLayer;
    protected Agent self;
    protected AgentController controller;
    protected AgentWeapons weapons;
    protected AgentHealth health;
    protected AgentStamina stamina;
    protected CharacterController charController;
    protected AudioManager audioManager;
    protected PoolManager poolManager;
    protected List<string> soundNames = new List<string>();
    protected AgentAnimEvents animEvents;
    protected NavMeshAgent navAgent;

    public Func<bool> Move => () => controller.Forwards || controller.Backwards || controller.Right || controller.Left;
    public Func<bool> Jump => () => controller.Jump;
    public Func<bool> Run => () => controller.Run;
    public Func<bool> Crouch => () => controller.Crouch;
    public Func<bool> OnGround => () => self.IsGrounded();
    public Func<bool> NextToWall => () => IsNextToWall();
    public Func<bool> Rising => () => charController.velocity.y > .01f;
    public Func<bool> Falling => () => charController.velocity.y < -.1f;
    public Func<bool> Attack => () => controller.Attack;
    public Func<bool> Block => () => controller.Block;
    public Func<bool> EquipWeaponInput => () => controller.Equipping;
    public Func<bool> MeleeEquipped => () => weapons.primarySlot.CurrentlyEquipped?.GetType() == typeof(MeleeWeapon);
    public Func<bool> RangedEquipped => () => weapons.primarySlot.CurrentlyEquipped?.GetType() == typeof(RangedWeapon) || weapons.secondarySlot.CurrentlyEquipped?.GetType() == typeof(RangedWeapon);
    public Func<bool> ShieldEquipped => () => weapons.secondarySlot.CurrentlyEquipped?.GetType() == typeof(Shield);
    public Func<bool> IsDead => () => health.IsDead;

    public AgentState(GameObject gameObject) : base(gameObject)
    {
        self = gameObject.GetComponent<Agent>();
        controller = gameObject.GetComponent<AgentController>();
        groundLayer = self.groundLayer;
        charController = gameObject.GetComponent<CharacterController>();
        weapons = gameObject.GetComponent<AgentWeapons>();
        health = gameObject.GetComponent<AgentHealth>();
        stamina = gameObject.GetComponent<AgentStamina>();
        anim = gameObject.GetComponentInChildren<Animator>();
        audioManager = AudioManager.instance;
        poolManager = PoolManager.Instance;
        animEvents = gameObject.GetComponentInChildren<AgentAnimEvents>();
        navAgent = gameObject.GetComponent<NavMeshAgent>();

        transitionsTo.Add(new Transition(typeof(Dying), IsDead));
        transitionsTo.Add(new Transition(typeof(TakingDamage), () => health.TookDamage));
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
            newVelocity += self.lookDirection.forward;
        }
        if (controller.Backwards)
        {
            newVelocity += -self.lookDirection.forward;
        }
        if (controller.Left)
        {
            newVelocity += -self.lookDirection.right;
        }
        if (controller.Right)
        {
            newVelocity += self.lookDirection.right;
        }
        newVelocity = newVelocity.normalized;
        return newVelocity;
    }

}
