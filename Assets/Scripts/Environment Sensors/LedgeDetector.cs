using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LedgeDetector : MonoBehaviour
{
    public int CollidingWith { get; private set; } = 0;

    private void OnTriggerEnter(Collider other)
    {
        CollidingWith++;
    }

    private void OnTriggerExit(Collider other)
    {
        CollidingWith--;
    }

    public void RaiseCollider(float amount)
    {
        transform.Translate(Vector3.up * amount);
    }
}
