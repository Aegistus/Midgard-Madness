using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Running : OnGroundState
{
    private float moveSpeed = 6f;
    private float staminaCost = 10f;
    private bool hasEnoughStamina = true;

    public Running(GameObject gameObject) : base(gameObject)
    {
        animationHash = Animator.StringToHash("Running");
        transitionsTo.Add(new Transition(typeof(Walking), Not(Run)));
        transitionsTo.Add(new Transition(typeof(Walking), () => !hasEnoughStamina));
        transitionsTo.Add(new Transition(typeof(Idling), Not(Move), Not(Run)));
        transitionsTo.Add(new Transition(typeof(MomentumAttacking), MeleeEquipped, Attack));
        transitionsTo.Add(new Transition(typeof(RangedAiming), RangedEquipped, Attack));
        transitionsTo.Add(new Transition(typeof(Equipping), EquipWeaponInput));
        transitionsTo.Add(new Transition(typeof(Falling), Not(OnGround)));
        transitionsTo.Add(new Transition(typeof(Blocking), Block));
    }

    public override void AfterExecution()
    {
        anim.SetBool(animationHash, false);
    }

    public override void BeforeExecution()
    {
        Debug.Log("Running");
        if (stamina.CurrentMoveStamina >= staminaCost)
        {
            anim.SetBool(animationHash, true);
            if (navAgent != null)
            {
                navAgent.speed = moveSpeed;
            }
            hasEnoughStamina = true;
        }
        else
        {
            hasEnoughStamina = false;
        }
 
    }

    Vector3 inputVelocity;
    public override void DuringExecution()
    {
        if (navAgent == null)
        {
            inputVelocity = GetAgentMovementInput();
            self.SetHorizontalVelocity(inputVelocity * moveSpeed);
            self.RotateAgentModelToDirection(inputVelocity);
            KeepGrounded();
        }
        if (stamina.CurrentMoveStamina >= staminaCost * Time.deltaTime)
        {
            stamina.DepleteMoveStamina(staminaCost * Time.deltaTime);
        }
        else
        {
            hasEnoughStamina = false;
        }
    }

}