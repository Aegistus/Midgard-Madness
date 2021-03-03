using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Vaulting : MovementState
{
    Func<bool> TimerUp => () => timer >= timerMax;

    float timerMax = 1f;
    float timer;

    float vaultSpeed = 3.5f;

    public Vaulting(GameObject gameObject) : base(gameObject)
    {
        animationHash = Animator.StringToHash("Vaulting");
        transitionsTo.Add(new Transition(typeof(Idling), TimerUp));
    }

    public override void AfterExecution()
    {
        if (TimerUp())
        {
            transform.position += movement.agentModel.forward * .25f;
        }
        charController.enabled = true;
        anim.SetLayerWeight(fullBodyLayer, 0);
        anim.SetBool(animationHash, false);
    }

    public override void BeforeExecution()
    {
        anim.SetLayerWeight(fullBodyLayer, 1);
        anim.SetBool(animationHash, true);
        timer = 0;
        charController.enabled = false;
    }

    public override void DuringExecution()
    {
        transform.Translate(movement.agentModel.forward * vaultSpeed * Time.deltaTime);
        timer += Time.deltaTime;
    }
}
