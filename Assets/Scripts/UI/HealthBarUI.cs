using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarUI : MonoBehaviour
{
    public Transform healthBar;

    private AgentHealth agent;


    private void Awake()
    {
        agent = GetComponentInParent<AgentHealth>();
    }

    private void Update()
    {
        healthBar.localScale = new Vector3(agent.CurrentHealth / 100f, 1, 1);
    }
}
