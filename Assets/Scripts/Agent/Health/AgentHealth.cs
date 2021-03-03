using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AgentHealth : MonoBehaviour
{
    public float maxHealth = 100f;

    public float CurrentHealth { get { return currentHealth; } }

    private float currentHealth;

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    public void Rpc_Damage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Cmd_Kill();
        }
    }

    public void Target_DamageNotification(float damage)
    {
        Debug.Log("You just received " + damage + " damage!");
    }

    public void Rpc_Heal(float health)
    {
        currentHealth += health;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        print(currentHealth);
    }

    public void Cmd_Kill()
    {
        Rpc_Kill();
    }

    public void Rpc_Kill()
    {
        gameObject.SetActive(false);
    }
}
