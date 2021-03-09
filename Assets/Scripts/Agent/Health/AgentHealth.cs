using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using CodeMonkey.Utils;

public class AgentHealth : MonoBehaviour
{
    public float maxHealth = 100f;

    public event Action OnAgentDeath;

    public bool IsDead { get; private set; } = false;
    public bool TookDamage { get; private set; } = false;
    public float CurrentHealth { get { return currentHealth; } }

    private float currentHealth;

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    public void Damage(float damage)
    {
        currentHealth -= damage;
        AudioManager.instance.PlaySoundGroupAtPosition("Taking Damage", transform.position);
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Kill();
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
    }
}
