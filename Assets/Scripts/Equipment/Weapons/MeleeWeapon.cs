using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class MeleeWeapon : Weapon
{
    public Collider blade;

    private MeleeWeaponStats MeleeStats => (MeleeWeaponStats)stats;
    private bool inDamageState = false;
    private float damageModifier = 1;
    private List<AgentHealth> hitAgents = new List<AgentHealth>();
    private Coroutine damageStateRoutine;

    public void EnterDamageState(float damageModifier, float duration)
    {
        EnterDamageState(damageModifier);
        damageStateRoutine = StartCoroutine(DamageState(duration));
    }

    public void EnterDamageState(float damageModifier)
    {
        inDamageState = true;
        this.damageModifier = damageModifier;
        hitAgents = new List<AgentHealth>();
    }

    public void ExitDamageState()
    {
        if (damageStateRoutine != null)
        {
            StopCoroutine(damageStateRoutine);
        }
        inDamageState = false;
        damageModifier = 1;
    }

    private IEnumerator DamageState(float duration)
    {
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
            Agent agent = other.GetComponentInParent<Agent>();
            if (agent != null && health != null)
            {
                // check to make sure not hitting self, multi-hitting one agent, or hitting someone with the same tag
                if (!hitAgents.Contains(health) && !transform.IsChildOf(health.transform) && !transform.root.CompareTag(health.tag))
                {
                    health.Damage(stats.damage * damageModifier, transform.position, MeleeStats.knockbackForce);
                    hitAgents.Add(health);
                    AudioManager.instance.PlaySoundAtPosition("Sword Hit", transform.position);
                }
            }
        }
    }

}
