using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedAiming : AgentState
{
    private Vector3 cameraShift = new Vector3(.75f, 1f, .75f);

    public RangedAiming(GameObject gameObject) : base(gameObject)
    {
        transitionsTo.Add(new Transition(typeof(RangedAttacking), RangedEquipped, Not(Attack)));
        animationHash = Animator.StringToHash("RangedAim");
    }

    public override void AfterExecution()
    {
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
        anim.SetBool(animationHash, true);
        self.SetHorizontalVelocity(Vector3.zero);
        if (controller.GetType() == typeof(PlayerController))
        {
            PlayerController player = (PlayerController)controller;
            player.ShiftCameraPosition(cameraShift);
        }
    }

    public override void DuringExecution()
    {
        self.RotateAgentModelToDirection(self.lookDirection.forward);
    }
}
