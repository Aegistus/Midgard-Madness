using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewAgentStats", menuName = "Agent Stats", order = 3)]
public class AgentStats : ScriptableObject
{
    [Header("Health")]
    public float maxHealth = 100;
    public float healthRegenRate = 2f;
    public float toughness = 20f; // Damage % needs to be greater than this percent in order to trigger a flinch

    [Header("Stamina")]
    public float maxStamina = 50f;
    public float staminaRegenRate = 5f;
    public float staminaRegenDelay = 2f;

    [Header("Stamina Costs")]
    public float runCost = 2;
    public float jumpCost = 10;
    public float rollCost = 20;

    [Header("Vigor")]
    public float maxVigor = 50;
    public float vigorRegenRate = 5f;
    public float vigorRegenDelay = 2f;

    [Header("Vigor Costs")]
    public float meleeAttackCost = 10;
    public float momentumAttackCost = 25;
    public float rangedAimCost = 2f;
    public float blockCost = 5f;

    [Header("Movement")]
    public float walkSpeed = 3;
    public float runSpeed = 6;
    public float rollSpeed = 5;
    public float dodgeBonusSpeed = 3f;
    public float attackSpeed = 1;
    public float jumpForce = 5;
    public float airMoveSpeed = 3.5f;
    
}
