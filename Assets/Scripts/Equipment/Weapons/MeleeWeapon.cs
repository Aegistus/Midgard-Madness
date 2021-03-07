using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : Weapon
{
    public float damage;
    public Collider blade;

    private bool inDamageState = false;
    private float damageModifier = 1;

    public void EnterDamageState(float duration, float damageModifier)
    {
        this.damageModifier = damageModifier;
        StartCoroutine(DamageState(duration));
    }

    private IEnumerator DamageState(float duration)
    {
        inDamageState = true;
        yield return new WaitForSeconds(duration);
        inDamageState = false;
        damageModifier = 1;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (inDamageState)
        {
            AgentHealth health = other.GetComponentInParent<AgentHealth>();
            if (health != null && !transform.IsChildOf(health.transform)) // check to make sure not hitting self
            {
                health.Damage(damage * damageModifier);
            }
        }
    }
}
