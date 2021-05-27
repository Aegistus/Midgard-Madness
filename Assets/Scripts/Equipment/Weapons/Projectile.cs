using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(SphereCollider))]
public class Projectile : MonoBehaviour
{
    public float force = 1f;
    public float damage = 10f;
    public float damageModifier = 1f;
    public float knockbackForce = 1f;
    public string startSoundName = "Arrow Loose";
    public string impactSoundName = "Projectile Impact";

    private Rigidbody rb;
    private List<AgentHealth> hitHealths = new List<AgentHealth>();
    private Vector3 startingPosition;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        startingPosition = transform.position;
        AudioManager.instance.PlaySoundAtPosition(startSoundName, transform.position);
        rb.AddForce(transform.forward * force);
    }

    private void OnTriggerEnter(Collider other)
    {
        AgentHealth health = other.GetComponentInParent<AgentHealth>();
        Agent agent = other.GetComponentInParent<Agent>();
        if (agent != null)
        {
            if (!hitHealths.Contains(health))
            {
                health.Damage(damage * damageModifier, startingPosition, knockbackForce);
                hitHealths.Add(health);
            }
        }
        AudioManager.instance.PlaySoundAtPosition(impactSoundName, transform.position);
        Destroy(gameObject);
    }
}
