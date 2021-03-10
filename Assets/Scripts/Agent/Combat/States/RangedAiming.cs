using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedAiming : AgentState
{
    //private Vector3 cameraShift = new Vector3(1.15f, 0, -1.5f);

    public RangedAiming(GameObject gameObject) : base(gameObject)
    {
        transitionsTo.Add(new Transition(typeof(RangedAttacking), RangedEquipped, Not(Attack)));
        animationHash = Animator.StringToHash("RangedAim");
        animEvents.OnAnimationEvent += CheckAnimationEvents;
    }

    private void CheckAnimationEvents(EventType type)
    {
        if (isCurrentState)
        {
            if (type == EventType.WeaponSound)
            {
                audioManager.PlaySoundAtPosition("Bow Draw", transform.position);
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
        Debug.Log("Aiming");
        isCurrentState = true;
        anim.SetBool(animationHash, true);
        self.SetHorizontalVelocity(Vector3.zero);
        //if (controller.GetType() == typeof(PlayerController))
        //{
        //    PlayerController player = (PlayerController)controller;
        //    player.ShiftCameraPosition(cameraShift);
        //}
    }

    public override void DuringExecution()
    {
        self.RotateAgentModelToDirection(self.lookDirection.forward);
    }
}
