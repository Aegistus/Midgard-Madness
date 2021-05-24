using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using CodeMonkey.Utils;

public class AgentHealth : MonoBehaviour
{
    public float MaxHealth => stats.maxHealth;
    public float Toughness => stats.toughness;

    public event Action OnAgentDeath;
    public event Action OnAgentTakeDamage;

    public bool IsDead { get; private set; } = false;
    public bool TookSignificatDamage { get; private set; } = false;
    public float CurrentHealth { get { return currentHealth; } }

    private float currentHealth;
    private Agent agent;
    private AgentMovement movement;
    private AgentStats stats;

    private void Awake()
    {
        agent = GetComponent<Agent>();
        movement = GetComponent<AgentMovement>();
        stats = agent.agentStats;
        currentHealth = MaxHealth;
    }

    public void Damage(float damage, Vector3 origin, float force)
    {
        currentHealth -= damage;
        movement.SetHorizontalVelocity((transform.position - origin) * force);
        AudioManager.instance.PlaySoundAtPosition("Taking Damage", transform.position);
        OnAgentTakeDamage?.Invoke();
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Kill();
        }
    }

    public void Heal(float amount)
    {
        currentHealth += amount;
        if (currentHealth > MaxHealth)
        {
            currentHealth = MaxHealth;
        }
    }

    public void Kill()
    {
        IsDead = true;
        OnAgentDeath?.Invoke();
    }

    float lastHealth;
    private void Update()
    {
        if (lastHealth / MaxHealth > currentHealth / MaxHealth + Toughness / 100)
        {
            TookSignificatDamage = true;
        }
        else
        {
            TookSignificatDamage = false;
        }
        lastHealth = currentHealth;
        if (!TookSignificatDamage && !IsDead)
        {
            Heal(stats.healthRegenRate * Time.deltaTime);
        }
    }
}
