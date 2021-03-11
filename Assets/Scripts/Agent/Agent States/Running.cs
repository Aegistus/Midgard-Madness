using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Running : OnGroundState
{
    public Running(GameObject gameObject) : base(gameObject)
    {
        animationHash = Animator.StringToHash("Running");
        transitionsTo.Add(new Transition(typeof(Walking), Not(Run)));
        transitionsTo.Add(new Transition(typeof(Idling), Not(Move), Not(Run)));
        transitionsTo.Add(new Transition(typeof(Walking), () => stamina.CurrentStamina < agentStats.runCost));
        transitionsTo.Add(new Transition(typeof(MomentumAttacking), MeleeEquipped, Attack, () => vigor.CurrentVigor >= agentStats.momentumAttackCost));
        transitionsTo.Add(new Transition(typeof(RangedAiming), RangedEquipped, Attack, () => vigor.CurrentVigor >= agentStats.rangedAimCost));
        transitionsTo.Add(new Transition(typeof(Equipping), EquipWeaponInput));
        transitionsTo.Add(new Transition(typeof(Falling), Not(OnGround)));
        transitionsTo.Add(new Transition(typeof(Blocking), Block, () => vigor.CurrentVigor >= agentStats.blockCost));
    }

    public override void AfterExecution()
    {
        anim.SetBool(animationHash, false);
    }

    public override void BeforeExecution()
    {
        Debug.Log("Running");
        anim.SetBool(animationHash, true);
        if (navAgent != null)
        {
            navAgent.speed = agentStats.runSpeed;
        }
    }

    Vector3 inputVelocity;
    public override void DuringExecution()
    {
        if (navAgent == null)
        {
            inputVelocity = GetAgentMovementInput();
            self.SetHorizontalVelocity(inputVelocity * agentStats.runSpeed);
            self.RotateAgentModelToDirection(inputVelocity);
            KeepGrounded();
        }
        stamina.DepleteStamina(agentStats.runCost * Time.deltaTime);
    }

}