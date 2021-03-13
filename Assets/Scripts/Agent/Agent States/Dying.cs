using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Dying : AgentState
{
    int animation = 0;
    int animationVariantsTotal = 5;

    public Dying(GameObject gameObject) : base(gameObject)
    {
        animationHash = Animator.StringToHash("Dying");
    }

    public override void AfterExecution()
    {

    }

    public override void BeforeExecution()
    {
        self.SetHorizontalVelocity(Vector3.zero);
        animation = Random.Range(0, animationVariantsTotal);
        anim.SetInteger(animationHash, animation);
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
