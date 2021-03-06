using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fighting : NPCState
{
    public Fighting(GameObject gameObject) : base(gameObject)
    {
        transitionsTo.Add(new Transition(typeof(Wandering), Not(PlayerInSight)));
    }

    public override void AfterExecution()
    {

    }

    public override void BeforeExecution()
    {
        Debug.Log("NPC Fighting");
    }

    protected override void CreateTree()
    {
        InverterNode noWeaponOut = new InverterNode(new ActionNode(() => HasWeaponEquipped()));
        ActionNode getWeaponOut = new ActionNode(() => controller.EquipWeapon());
        SequenceNode equipSequence = new SequenceNode(new List<Node>() { noWeaponOut, getWeaponOut });

        InverterNode notNearTarget = new InverterNode(new ActionNode(() => AtDestination(1f)));
        ActionNode moveToTarget = new ActionNode(() => controller.SetDestination(fov.visibleTargets[0].position));
        SequenceNode chaseSequence = new SequenceNode(new List<Node>() { notNearTarget, moveToTarget });

        ActionNode attackTarget = new ActionNode(() => controller.AttackEnemy());

        rootNode = new SelectorNode(new List<Node>() { equipSequence, chaseSequence, attackTarget });
    }

}
