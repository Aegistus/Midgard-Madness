using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedAttacking : AgentState
{
    private bool animationFinished = false;
    private RangedWeapon weapon;

    public RangedAttacking(GameObject gameObject) : base(gameObject)
    {
        transitionsTo.Add(new Transition(typeof(Idling), () => animationFinished));
        animationHash = Animator.StringToHash("RangedAttack");
        animEvents.OnAnimationEvent += CheckAnimationEvent;
    }

    void CheckAnimationEvent(EventType eventType)
    {
        if (isCurrentState)
        {
            if (eventType == EventType.Finish)
            {
                animationFinished = true;
            }
            else if (eventType == EventType.WeaponSound)
            {
                audioManager.PlaySoundAtPosition("Bow Loose", transform.position);
            }
        }
    }

    public override void AfterExecution()
    {
        isCurrentState = false;
        anim.SetBool(animationHash, false);
        //if (controller.GetType() == typeof(PlayerController))
        //{
        //    PlayerController player = (PlayerController)controller;
        //    player.ResetCameraPosition();
        //}
    }

    public override void BeforeExecution()
    {
        Debug.Log("Shooting");
        isCurrentState = true;
        animationFinished = false;
        if (weapons.primarySlot.CurrentlyEquipped?.GetType() == typeof(RangedWeapon))
        {
            weapon = (RangedWeapon)weapons.primarySlot.CurrentlyEquipped;
        }
        else if (weapons.secondarySlot.CurrentlyEquipped?.GetType() == typeof(RangedWeapon))
        {
            weapon = (RangedWeapon)weapons.secondarySlot.CurrentlyEquipped;
        }
        anim.SetBool(animationHash, true);
        self.SetHorizontalVelocity(Vector3.zero);
        if (weapon != null)
        {
            weapon.RangedAttack(controller.Aim);
        }
    }

    public override void DuringExecution()
    {

    }
}
