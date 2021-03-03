using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AttackingState : CombatState
{
    protected AgentAnimEvents animEvents;

    private bool animationFinished = false;

    public Func<bool> AnimationFinished => () => animationFinished;

    public AttackingState(GameObject gameObject) : base(gameObject)
    {
        transitionsTo.Add(new Transition(typeof(ReadyState), AnimationFinished));
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
        Debug.Log("Releasing");
        anim.SetBool(animationHash, true);
        animationFinished = false;
    }

    public override void DuringExecution()
    {

    }
}
