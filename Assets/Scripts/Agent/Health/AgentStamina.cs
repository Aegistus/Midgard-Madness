using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentStamina : MonoBehaviour
{
    public float CurrentStamina { get; private set; }

    public float MaxStamina => stats.maxStamina;
    private float staminaTimer = 0;

    private AgentStats stats;

    private void Start()
    {
        stats = gameObject.GetComponent<Agent>().agentStats;
        CurrentStamina = MaxStamina;
        staminaTimer = stats.staminaRegenDelay;
    }

    public void DepleteStamina(float amount)
    {
        CurrentStamina -= amount;
        if (CurrentStamina < 0)
        {
            CurrentStamina = 0;
        }
        staminaTimer = 0;
    }

    public void ReplenishStamina(float amount)
    {
        if (CurrentStamina < MaxStamina)
        {
            CurrentStamina += amount;
            if (CurrentStamina > MaxStamina)
            {
                CurrentStamina = MaxStamina;
            }
        }
    }

    private void Update()
    {
        if (staminaTimer >= stats.staminaRegenDelay)
        {
            ReplenishStamina(stats.staminaRegenRate * Time.deltaTime);
        }
        else
        {
            staminaTimer += Time.deltaTime;
        }
    }
}
