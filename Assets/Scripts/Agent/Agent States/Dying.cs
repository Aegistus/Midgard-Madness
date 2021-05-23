using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Dying : AgentState
{

    public Dying(GameObject gameObject) : base(gameObject)
    {

    }

    public override void AfterExecution()
    {

    }

    public override void BeforeExecution()
    {
        self.SetHorizontalVelocity(Vector3.zero);
        if (self.agentSounds)
        {
            audio.clip = self.agentSounds.death.GetRandomAudioClip();
            audio.loop = false;
            audio.Play();
        }
        if(navAgent)
        {
            navAgent.enabled = false;
        }
        charController.enabled = false;
        gameObject.GetComponentInChildren<HealthBarUI>()?.gameObject.SetActive(false);
        gameObject.GetComponentInChildren<HealthBarUI>()?.gameObject.SetActive(false);
        Collider[] colliders = gameObject.GetComponentsInChildren<Collider>();
        foreach (var collider in colliders)
        {
            collider.enabled = false;
        }
    }

    public override void DuringExecution()
    {
    }
}
