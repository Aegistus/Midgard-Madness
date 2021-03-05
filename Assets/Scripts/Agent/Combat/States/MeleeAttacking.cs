using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MeleeAttacking : AgentState
{
    protected AgentAnimEvents animEvents;

    private bool animationFinished = false;

    public MeleeAttacking(GameObject gameObject) : base(gameObject)
    {
        transitionsTo.Add(new Transition(typeof(Idling), () => animationFinished));
        animationHash = Animator.StringToHash("MeleeAttack");
        animEvents = gameObject.GetComponentInChildren<AgentAnimEvents>();
        animEvents.OnAnimationEvent += CheckAnimationEvent;
    }

    private void CheckAnimationEvent(EventType eventType)
    {
        if (eventType == EventType.Finish)
        {
            animationFinished = true;
        }
    }

    public override void AfterExecution()
    {
        anim.SetBool(animationHash, false);
    }

    public override void BeforeExecution()
    {
        Debug.Log("Melee Attack");
        anim.SetBool(animationHash, true);
        animationFinished = false;
        movement.SetHorizontalVelocity(Vector3.zero);
    }

    public override void DuringExecution()
    {

    }
}
