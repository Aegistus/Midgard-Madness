using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EquippingPrimary : CombatState
{
    private bool animationDone = false;
    private AgentAnimEvents animEvents;
    private AgentWeapons equipment;

    public EquippingPrimary(GameObject gameObject) : base(gameObject)
    {
        animationHash = Animator.StringToHash("Equipping");
        transitionsTo.Add(new Transition(typeof(ReadyState), () => animationDone));
        equipment = gameObject.GetComponent<AgentWeapons>();
        animEvents = gameObject.GetComponentInChildren<AgentAnimEvents>();
    }

    private void EnableNewWeapon(EventType eventType)
    {
        if (eventType == EventType.Finish)
        {
            equipment.GoToNextPrimaryEquipment();
            animationDone = true;
        }
    }

    public override void AfterExecution()
    {
        anim.SetBool(animationHash, false);
        animEvents.OnAnimationEvent -= EnableNewWeapon;
    }

    public override void BeforeExecution()
    {
        anim.SetBool(animationHash, true);
        animationDone = false;
        animEvents.OnAnimationEvent += EnableNewWeapon;
    }

    public override void DuringExecution()
    {

    }
}
