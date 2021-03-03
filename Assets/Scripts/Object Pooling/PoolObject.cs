using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolObject : MonoBehaviour
{
    public float lifeTime;

    private ParticleSystem particles;
    private AudioSource audioSource;
    //private IEffect effect;
    private bool justSpawned = true;

    private void Awake()
    {
        particles = GetComponent<ParticleSystem>();
        audioSource = GetComponent<AudioSource>();
        //effect = GetComponent<IEffect>();
    }

    protected virtual void OnEnable()
    {
        if (!justSpawned)
        {
            if (particles)
            {
                particles.Play();
            }
            if (audioSource)
            {
                audioSource.Play();
            }
            //if (effect != null)
            //{
            //    effect.StartEffect();
            //}
            StartCoroutine(EndLifeTime());
        }
        else
        {
            justSpawned = false;
        }
    }

    private IEnumerator EndLifeTime()
    {
        yield return new WaitForSeconds(lifeTime);
        gameObject.SetActive(false);
    }
}
