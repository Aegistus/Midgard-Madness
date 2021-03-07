using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Equipping : AgentState
{
    private bool animationDone = false;
    private AgentAnimEvents animEvents;

    public Equipping(GameObject gameObject) : base(gameObject)
    {
        animationHash = Animator.StringToHash("Equipping");
        transitionsTo.Add(new Transition(typeof(Idling), () => animationDone));
        weapons = gameObject.GetComponent<AgentWeapons>();
        animEvents = gameObject.GetComponentInChildren<AgentAnimEvents>();
    }

    private void EnableNewWeapon(EventType eventType)
    {
        if (eventType == EventType.Finish)
        {
            weapons.EquipWeapon(controller.WeaponNumKey);
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
        movement.SetHorizontalVelocity(Vector3.zero);
        audioManager.PlaySoundGroupAtPosition("Blade Equip", transform.position);
    }

    public override void DuringExecution()
    {

    }
}
