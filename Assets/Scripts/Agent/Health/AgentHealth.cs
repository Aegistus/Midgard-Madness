using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using CodeMonkey.Utils;

public class AgentHealth : MonoBehaviour
{
    public float MaxHealth => stats.maxHealth;

    public event Action OnAgentDeath;
    public event Action OnAgentTakeDamage;

    public bool IsDead { get; private set; } = false;
    public bool TookDamage { get; private set; } = false;
    public float CurrentHealth { get { return currentHealth; } }

    private float currentHealth;
    private Agent agent;
    private AgentStats stats;

    private void Awake()
    {
        agent = GetComponent<Agent>();
        stats = agent.agentStats;
        currentHealth = MaxHealth;
    }

    public void Damage(float damage, Vector3 origin, float force)
    {
        currentHealth -= damage;
        agent.SetHorizontalVelocity((transform.position - origin) * force);
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
        if (lastHealth > currentHealth)
        {
            TookDamage = true;
        }
        else
        {
            TookDamage = false;
        }
        lastHealth = currentHealth;
        if (!TookDamage && !IsDead)
        {
            Heal(stats.healthRegenRate * Time.deltaTime);
        }
    }
}
