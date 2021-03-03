using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Climbing : MovementState
{
    Func<bool> TimerUp => () => timer >= timerMax;

    float timerMax = 3f;
    float timer;

    public Climbing(GameObject gameObject) : base(gameObject)
    {
        animationHash = Animator.StringToHash("Climbing");
        transitionsTo.Add(new Transition(typeof(Idling), TimerUp));
    }

    public override void AfterExecution()
    {
        if (TimerUp())
        {
            transform.position += movement.agentModel.forward + (Vector3.up * 1.4f);
        }
        charController.enabled = true;
        anim.SetLayerWeight(fullBodyLayer, 0);
        anim.SetBool(animationHash, false);
    }

    public override void BeforeExecution()
    {
        Debug.Log("Climbing");
        anim.SetLayerWeight(fullBodyLayer, 1);
        anim.SetBool(animationHash, true);
        timer = 0;
        transform.position += movement.agentModel.forward * .2f;
        charController.enabled = false;
    }

    public override void DuringExecution()
    {
        timer += Time.deltaTime;
    }
}
