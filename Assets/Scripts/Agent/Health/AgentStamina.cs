using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentStamina : MonoBehaviour
{
    public float CurrentMoveStamina { get; private set; }
    public float CurrentAttackStamina { get; private set; }

    [SerializeField] private float maxStamina = 100;
    [SerializeField] private float replenishRate = 2f;
    [SerializeField] private float replenishDelay = 1f;
    private float attackStaminaTimer = 0;
    private float moveStaminaTimer = 0;

    private void Start()
    {
        CurrentMoveStamina = maxStamina;
        CurrentAttackStamina = maxStamina;
        attackStaminaTimer = replenishDelay;
        moveStaminaTimer = replenishDelay;
    }

    public void DepleteMoveStamina(float amount)
    {
        CurrentMoveStamina -= amount;
        if (CurrentMoveStamina < 0)
        {
            CurrentMoveStamina = 0;
        }
        moveStaminaTimer = 0;
    }

    public void ReplenishMoveStamina(float amount)
    {
        if (CurrentMoveStamina < maxStamina)
        {
            CurrentMoveStamina += amount;
            if (CurrentMoveStamina > maxStamina)
            {
                CurrentMoveStamina = maxStamina;
            }
        }
    }

    public void DepleteAttackStamina(float amount)
    {
        CurrentAttackStamina -= amount;
        if (CurrentAttackStamina < 0)
        {
            CurrentAttackStamina = 0;
        }
        attackStaminaTimer = 0;
    }

    public void ReplenishAttackStamina(float amount)
    {
        if (CurrentAttackStamina < maxStamina)
        {
            CurrentAttackStamina += amount;
            if (CurrentAttackStamina > maxStamina)
            {
                CurrentAttackStamina = maxStamina;
            }
        }
    }

    private void Update()
    {
        if (moveStaminaTimer >= replenishDelay)
        {
            ReplenishMoveStamina(replenishRate * Time.deltaTime);
        }
        else
        {
            moveStaminaTimer += Time.deltaTime;
        }
        if (attackStaminaTimer >= replenishDelay)
        {
            ReplenishAttackStamina(replenishRate * Time.deltaTime);
        }
        else
        {
            attackStaminaTimer += Time.deltaTime;
        }
        print(CurrentMoveStamina);
    }
}
