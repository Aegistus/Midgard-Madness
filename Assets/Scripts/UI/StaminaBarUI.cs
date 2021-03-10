using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaminaBarUI : MonoBehaviour
{
    public Transform attackStaminaBar;
    public Transform moveStaminaBar;

    private AgentStamina stamina;

    private void Start()
    {
        stamina = GameObject.FindGameObjectWithTag("Player").GetComponent<AgentStamina>();
    }

    private void Update()
    {
        attackStaminaBar.localScale = new Vector3(stamina.CurrentAttackStamina / stamina.maxStamina, 1, 1);
        moveStaminaBar.localScale = new Vector3(stamina.CurrentMoveStamina / stamina.maxStamina, 1, 1);
    }
}
