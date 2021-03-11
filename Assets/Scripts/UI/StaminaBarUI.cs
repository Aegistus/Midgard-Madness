using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaminaBarUI : MonoBehaviour
{
    public Transform staminaBar;

    private AgentStamina stamina;

    private void Start()
    {
        stamina = GameObject.FindGameObjectWithTag("Player").GetComponent<AgentStamina>();
    }

    private void Update()
    {
        staminaBar.localScale = new Vector3(stamina.CurrentStamina / stamina.MaxStamina, 1, 1);
    }
}
