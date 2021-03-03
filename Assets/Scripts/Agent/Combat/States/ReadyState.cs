using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReadyState : CombatState
{

    public ReadyState(GameObject gameObject) : base(gameObject)
    {
        transitionsTo.Add(new Transition(typeof(Blocking), BlockInput));
        transitionsTo.Add(new Transition(typeof(RightSlashing), MeleeEquipped, AttackInput, RightSlashInput));
        transitionsTo.Add(new Transition(typeof(LeftSlashing), MeleeEquipped, AttackInput, LeftSlashInput));
        transitionsTo.Add(new Transition(typeof(Stabbing), MeleeEquipped, AttackInput, StabInput));
        transitionsTo.Add(new Transition(typeof(Drawing), RangedEquipped, AttackInput));
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
