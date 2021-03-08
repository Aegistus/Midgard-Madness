using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Idling : OnGroundState
{

    public Idling(GameObject gameObject) : base(gameObject)
    {
        animationHash = Animator.StringToHash("Idling");
        transitionsTo.Add(new Transition(typeof(Walking), Move));
        transitionsTo.Add(new Transition(typeof(Falling), Not(OnGround)));
        transitionsTo.Add(new Transition(typeof(Blocking), Block));

        transitionsTo.Add(new Transition(typeof(MeleeAttacking), MeleeEquipped, Attack));
        transitionsTo.Add(new Transition(typeof(RangedAiming), RangedEquipped, Attack));
        transitionsTo.Add(new Transition(typeof(Equipping), EquipWeaponInput));
    }

    public override void AfterExecution()
    {
        anim.SetBool(animationHash, false);
    }

    public override void BeforeExecution()
    {
        Debug.Log("Idling");
        anim.SetBool(animationHash, true);
        self.SetHorizontalVelocity(Vector3.zero);
        self.SetVerticalVelocity(0);
        KeepGrounded();
    }

    public override void DuringExecution()
    {
        
    }
}
