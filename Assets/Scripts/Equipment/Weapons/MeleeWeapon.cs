﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : Weapon
{
    public Collider blade;

    private bool inDamageState = false;
    private float damageModifier = 1;
    private List<AgentHealth> hitAgents = new List<AgentHealth>();
    private Coroutine damageStateRoutine;

    public void EnterDamageState(float duration, float damageModifier)
    {
        this.damageModifier = damageModifier;
        hitAgents = new List<AgentHealth>();
        damageStateRoutine = StartCoroutine(DamageState(duration));
    }

    public void AbortDamageState()
    {
        StopCoroutine(damageStateRoutine);
        inDamageState = false;
        damageModifier = 1;
    }

    private IEnumerator DamageState(float duration)
    {
        inDamageState = true;
        yield return new WaitForSeconds(duration);
        inDamageState = false;
        damageModifier = 1;
    }

    // for attacking
    private void OnTriggerEnter(Collider other)
    {
        if (inDamageState)
        {
            AgentHealth health = other.GetComponentInParent<AgentHealth>();
            // check to make sure not hitting self or multi-hitting one agent
            if (health != null && !hitAgents.Contains(health) && !transform.IsChildOf(health.transform))
            {
                health.Damage(stats.damage * damageModifier);
                hitAgents.Add(health);
                AudioManager.instance.PlaySoundGroupAtPosition("Sword Hit", transform.position);
                PoolManager.Instance.GetObjectFromPoolWithLifeTime(PoolManager.PoolTag.Blood, other.ClosestPoint(transform.position), Quaternion.identity, 3f);
            }
        }
    }
}
