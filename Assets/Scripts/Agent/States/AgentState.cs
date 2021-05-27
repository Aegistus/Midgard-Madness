using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class AgentState : State
{
    protected LayerMask groundLayer;
    protected Agent self;
    protected AgentMovement movement;
    protected AgentStats agentStats;
    protected AgentWeapons weapons;
    protected AgentHealth health;
    protected AgentStamina stamina;
    protected AgentVigor vigor;
    protected CharacterController charController;
    protected AudioManager audioManager;
    protected PoolManager poolManager;
    protected List<string> soundNames = new List<string>();
    protected AgentAnimEvents animEvents;
    protected NavMeshAgent navAgent;
    protected AudioSource audio;

    protected bool isCurrentState = false;

    public Func<bool> Move => () => self.Forwards || self.Backwards || self.Right || self.Left;
    public Func<bool> Jump => () => self.Jump;
    public Func<bool> Run => () => self.Run;
    public Func<bool> Crouch => () => self.Crouch;
    public Func<bool> OnGround => () => movement.IsGrounded();
    public Func<bool> NextToWall => () => IsNextToWall();
    public Func<bool> Rising => () => charController.velocity.y > .01f;
    public Func<bool> Falling => () => charController.velocity.y < -.1f;
    public Func<bool> Attack => () => self.Attack;
    public Func<bool> Block => () => self.Block;
    public Func<bool> EquipWeaponInput => () => self.Equipping;
    public Func<bool> UnEquipWeapon => () => self.UnEquipping;
    public Func<bool> MeleeEquipped => () => weapons.primarySlot.CurrentlyEquipped?.GetType() == typeof(MeleeWeapon);
    public Func<bool> RangedEquipped => () => weapons.primarySlot.CurrentlyEquipped?.GetType() == typeof(RangedWeapon) || weapons.secondarySlot.CurrentlyEquipped?.GetType() == typeof(RangedWeapon);
    public Func<bool> ShieldEquipped => () => weapons.secondarySlot.CurrentlyEquipped?.GetType() == typeof(Shield);
    public Func<bool> IsDead => () => health.IsDead;

    public AgentState(GameObject gameObject) : base(gameObject)
    {
        self = gameObject.GetComponent<Agent>();
        movement = gameObject.GetComponent<AgentMovement>();
        agentStats = self.agentStats;
        groundLayer = movement.groundLayer;
        charController = gameObject.GetComponent<CharacterController>();
        weapons = gameObject.GetComponent<AgentWeapons>();
        health = gameObject.GetComponent<AgentHealth>();
        stamina = gameObject.GetComponent<AgentStamina>();
        vigor = gameObject.GetComponent<AgentVigor>();
        audioManager = AudioManager.instance;
        poolManager = PoolManager.Instance;
        animEvents = gameObject.GetComponentInChildren<AgentAnimEvents>();
        navAgent = gameObject.GetComponent<NavMeshAgent>();
        audio = gameObject.GetComponentInChildren<AudioSource>();

        transitionsTo.Add(new Transition(typeof(Dying), IsDead));
        transitionsTo.Add(new Transition(typeof(TakingDamage), () => health.TookSignificantDamage));
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
        if (self.Forwards)
        {
            newVelocity += movement.lookDirection.forward;
        }
        if (self.Backwards)
        {
            newVelocity += -movement.lookDirection.forward;
        }
        if (self.Left)
        {
            newVelocity += -movement.lookDirection.right;
        }
        if (self.Right)
        {
            newVelocity += movement.lookDirection.right;
        }
        newVelocity = newVelocity.normalized;
        return newVelocity;
    }

}
