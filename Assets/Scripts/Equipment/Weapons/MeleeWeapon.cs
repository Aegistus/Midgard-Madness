using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : Weapon
{
    public Collider blade;

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
            if (agent.CurrentState.GetType() != typeof(Blocking))
            {
                // check to make sure not hitting self or multi-hitting one agent
                if (health != null && !hitAgents.Contains(health) && !transform.IsChildOf(health.transform))
                {
                    health.Damage(stats.damage * damageModifier);
                    hitAgents.Add(health);
                    AudioManager.instance.PlaySoundGroupAtPosition("Sword Hit", transform.position);
                    PoolManager.Instance.GetObjectFromPoolWithLifeTime(PoolManager.PoolTag.Blood, other.ClosestPoint(transform.position), Quaternion.identity, 3f);
                }
            }
            else
            {
                print("Attack Blocked");
                ExitDamageState();
                AudioManager.instance.PlaySoundGroupAtPosition("Sword Block", transform.position);
                PoolManager.Instance.GetObjectFromPoolWithLifeTime(PoolManager.PoolTag.Spark, other.ClosestPoint(transform.position), Quaternion.identity, 1f);
            }

        }
    }
}
