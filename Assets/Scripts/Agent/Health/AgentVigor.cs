using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentVigor : MonoBehaviour
{
    public float CurrentVigor { get; private set; }

    public float MaxVigor => stats.maxVigor;
    private float vigorTimer = 0;

    private AgentStats stats;

    private void Start()
    {
        stats = gameObject.GetComponent<Agent>().agentStats;
        CurrentVigor = MaxVigor;
        vigorTimer = stats.vigorRegenDelay;
    }

    public void DepleteVigor(float amount)
    {
        CurrentVigor -= amount;
        if (CurrentVigor < 0)
        {
            CurrentVigor = 0;
        }
        vigorTimer = 0;
    }

    public void ReplenishVigor(float amount)
    {
        if (CurrentVigor < MaxVigor)
        {
            CurrentVigor += amount;
            if (CurrentVigor > MaxVigor)
            {
                CurrentVigor = MaxVigor;
            }
        }
    }

    private void Update()
    {
        if (vigorTimer >= stats.vigorRegenDelay)
        {
            ReplenishVigor(stats.vigorRegenRate * Time.deltaTime);
        }
        else
        {
            vigorTimer += Time.deltaTime;
        }
    }
}
