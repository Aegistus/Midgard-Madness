using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AgentHealth : MonoBehaviour
{
    public Transform agentModel;

    public float MaxHealth => stats.maxHealth;
    public float Toughness => stats.toughness;

    public event Action OnAgentDeath;
    public event Action OnAgentTakeDamage;

    public bool IsDead { get; private set; } = false;
    public bool TookSignificantDamage { get; private set; } = false;
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
        Vector3 attackDirection = (origin - agentModel.position).normalized;
        float attackAngle = Vector3.Angle(agentModel.forward, attackDirection);
        if (agent.CurrentState.GetType() == typeof(Blocking) && attackAngle < 90 && attackAngle > -90) 
        {
            AudioManager.instance.PlaySoundAtPosition("Sword Block", transform.position);
            PoolManager.Instance.GetObjectFromPoolWithLifeTime(PoolManager.PoolTag.Spark, transform.position, Quaternion.identity, 1f);
        }
        else
        {
            currentHealth -= damage;
            //movement.SetHorizontalVelocity((transform.position - origin) * force);
            AudioManager.instance.PlaySoundAtPosition("Taking Damage", transform.position);
            PoolManager.Instance.GetObjectFromPoolWithLifeTime(PoolManager.PoolTag.Blood, transform.position, Quaternion.identity, 3f);
            OnAgentTakeDamage?.Invoke();
            if (currentHealth <= 0)
            {
                currentHealth = 0;
                Kill();
            }
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
            TookSignificantDamage = true;
        }
        else
        {
            TookSignificantDamage = false;
        }
        lastHealth = currentHealth;
        if (!TookSignificantDamage && !IsDead)
        {
            Heal(stats.healthRegenRate * Time.deltaTime);
        }
    }
}
