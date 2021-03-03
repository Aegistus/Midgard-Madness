using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Equipping : CombatState
{
    private bool animationDone = false;
    private AgentAnimEvents animEvents;
    private AgentWeapons weapons;

    public Equipping(GameObject gameObject) : base(gameObject)
    {
        animationHash = Animator.StringToHash("Equipping");
        transitionsTo.Add(new Transition(typeof(ReadyState), () => animationDone));
        weapons = gameObject.GetComponent<AgentWeapons>();
        animEvents = gameObject.GetComponentInChildren<AgentAnimEvents>();
    }

    private void EnableNewWeapon(EventType eventType)
    {
        if (eventType == EventType.Finish)
        {
            weapons.EquipWeapon(agentController.WeaponNumKey);
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
