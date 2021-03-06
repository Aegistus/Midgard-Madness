using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AgentHealth : MonoBehaviour
{
    public float maxHealth = 100f;

    public event Action OnAgentDeath;

    public float CurrentHealth { get { return currentHealth; } }

    private float currentHealth;

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    public void Damage(float damage)
    {
        currentHealth -= damage;
        print("Hit: " + name + " for " + damage + " damage");
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Kill();
        }
    }

    public void Kill()
    {
        gameObject.SetActive(false);
        OnAgentDeath?.Invoke();
    }
}
