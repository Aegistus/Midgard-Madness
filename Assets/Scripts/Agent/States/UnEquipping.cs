using System.Collections;
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

    private void DisableWeapon(EventType eventType)
    {
        if (eventType == EventType.Finish)
        {
            animationDone = true;
            weapons.UnEquipAll();
        }
    }

    public override void AfterExecution()
    {
        animEvents.OnAnimationEvent -= DisableWeapon;
    }

    public override void BeforeExecution()
    {
        Debug.Log("UnEquipping");
        animationDone = false;
        animEvents.OnAnimationEvent += DisableWeapon;
        movement.SetHorizontalVelocity(movement.Velocity * .5f);
        audioManager.PlaySoundAtPosition("Blade Equip", transform.position);
    }

    public override void DuringExecution()
    {

    }
}
