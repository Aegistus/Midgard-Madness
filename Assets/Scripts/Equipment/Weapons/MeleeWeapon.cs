using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : Weapon
{
    public float damage;
    public Collider blade;

    private bool inDamageState = false;

    public void EnterDamageState(float duration)
    {
        StartCoroutine(DamageState(duration));
    }

    private IEnumerator DamageState(float duration)
    {
        inDamageState = true;
        yield return new WaitForSeconds(duration);
        inDamageState = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        print("hit something");
        if (inDamageState)
        {
            AgentHealth health = other.GetComponentInParent<AgentHealth>();
            if (health != null)
            {
                health.Damage(damage);
                print("Hit: " + health.name + " for " + damage + " damage");
            }
        }
    }
}
