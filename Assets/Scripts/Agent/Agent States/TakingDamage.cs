using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakingDamage : AgentState
{
    float timer = 0;
    float maxTimer = 1f;

    public TakingDamage(GameObject gameObject) : base(gameObject)
    {
        animationHash = Animator.StringToHash("TakingDamage");
        transitionsTo.Add(new Transition(typeof(Idling), () => timer <= 0));
    }

    public override void AfterExecution()
    {
        anim.SetBool(animationHash, false);
        audio.Stop();
        if (navAgent)
        {
            navAgent.enabled = true;
        }
    }

    public override void BeforeExecution()
    {
        timer = maxTimer;
        anim.SetBool(animationHash, true);
        if (self.agentSounds)
        {
            audio.clip = self.agentSounds.hit.GetRandomAudioClip();
            audio.loop = false;
            audio.Play();
        }
        if (navAgent)
        {
            navAgent.enabled = false;
        }
        Debug.Log("Taking Damage");
    }

    public override void DuringExecution()
    {
        timer -= Time.deltaTime;
    }
}
