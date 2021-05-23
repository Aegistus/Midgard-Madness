﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnEquipping : AgentState
{
    private bool animationDone = false;

    public UnEquipping(GameObject gameObject) : base(gameObject)
    {
        transitionsTo.Add(new Transition(typeof(Idling), () => animationDone));
        weapons = gameObject.GetComponent<AgentWeapons>();
        animEvents = gameObject.GetComponentInChildren<AgentAnimEvents>();
    }

    private void EnableNewWeapon(EventType eventType)
    {
        if (eventType == EventType.Finish)
        {
            animationDone = true;
        }
    }

    public override void AfterExecution()
    {
        animEvents.OnAnimationEvent -= EnableNewWeapon;
    }

    public override void BeforeExecution()
    {
        Debug.Log("UnEquipping");
        weapons.UnEquipAll();
        animationDone = false;
        animEvents.OnAnimationEvent += EnableNewWeapon;
        self.SetHorizontalVelocity(self.Velocity * .5f);
        audioManager.PlaySoundAtPosition("Blade Equip", transform.position);
    }

    public override void DuringExecution()
    {

    }
}
