using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReadyState : CombatState
{

    public ReadyState(GameObject gameObject) : base(gameObject)
    {
        transitionsTo.Add(new Transition(typeof(Blocking), BlockInput));

        transitionsTo.Add(new Transition(typeof(MeleeAttacking), MeleeEquipped, AttackInput));
        transitionsTo.Add(new Transition(typeof(RangedAiming), RangedEquipped, AttackInput));
        transitionsTo.Add(new Transition(typeof(Equipping), EquipWeaponInput));
    }

    public override void AfterExecution()
    {

    }

    public override void BeforeExecution()
    {
        Debug.Log("Ready");
    }

    public override void DuringExecution()
    {

    }
}
