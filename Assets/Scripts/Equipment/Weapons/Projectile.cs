using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(SphereCollider))]
public class Projectile : MonoBehaviour
{
    public float speed = 1f;
    public float damage = 10f;
    public float damageModifier = 1f;

    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        rb.AddForce(transform.forward * speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        AgentHealth health = other.GetComponentInParent<AgentHealth>();
        Agent agent = other.GetComponentInParent<Agent>();
        if (agent != null)
        {
            if (agent.CurrentState.GetType() != typeof(Blocking))
            {
                health.Damage(damage * damageModifier);
            }
            else
            {
                print("Projectile Blocked");
                AudioManager.instance.PlaySoundGroupAtPosition("Sword Block", transform.position);
                PoolManager.Instance.GetObjectFromPoolWithLifeTime(PoolManager.PoolTag.Spark, other.ClosestPoint(transform.position), Quaternion.identity, 1f);
            }
        }
        Destroy(gameObject);
    }
}
