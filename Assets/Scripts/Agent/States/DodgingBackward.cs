using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DodgingBackward : OnGroundState
{
    bool finished = false;

    public DodgingBackward(GameObject gameObject) : base(gameObject)
    {
        transitionsTo.Add(new Transition(typeof(Idling), () => finished));
        animEvents.OnAnimationEvent += CheckAnimationEvent;
    }

    private void CheckAnimationEvent(EventType animEvent)
    {
        if (animEvent == EventType.Finish)
        {
            finished = true;
        }
    }

    public override void AfterExecution()
    {
    }

    public override void BeforeExecution()
    {
        finished = false;
        movement.SetHorizontalVelocity(-movement.agentModel.forward * self.agentStats.dodgeBonusSpeed);
    }

    public override void DuringExecution()
    {
        movement.RotateAgentModelToDirection(movement.lookDirection.forward);
    }
}
