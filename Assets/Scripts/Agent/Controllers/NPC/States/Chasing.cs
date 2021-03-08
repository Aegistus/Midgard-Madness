using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chasing : NPCState
{
    public Chasing(GameObject gameObject) : base(gameObject)
    {
        transitionsTo.Add(new Transition(typeof(Wandering), Not(PlayerInSight)));
        transitionsTo.Add(new Transition(typeof(Fighting), () => AtDestination(controller.attackRadius) == NodeState.SUCCESS));
    }

    public override void AfterExecution()
    {

    }

    public override void BeforeExecution()
    {
        Debug.Log("NPC Chasing");
    }

    protected override void CreateTree()
    {
        InverterNode noWeaponOut = new InverterNode(new ActionNode(() => HasWeaponEquipped()));
        ActionNode getWeaponOut = new ActionNode(() => controller.EquipWeapon());
        SequenceNode equipSequence = new SequenceNode(new List<Node>() { noWeaponOut, getWeaponOut });

        InverterNode notNearTarget = new InverterNode(new ActionNode(() => AtDestination(1f)));
        ActionNode moveToTarget = new ActionNode(() => controller.SetDestination(fov.visibleTargets[0].position, true));
        SequenceNode chaseSequence = new SequenceNode(new List<Node>() { notNearTarget, moveToTarget });
    }
}
