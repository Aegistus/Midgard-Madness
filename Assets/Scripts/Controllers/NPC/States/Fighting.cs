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
        ActionNode attackTargetNode = new ActionNode(() => controller.AttackEnemy());
        ActionNode nearTargetNode = new ActionNode(() => AtDestination(1f));
        SequenceNode attackSequence = new SequenceNode(new List<Node>() { attackTargetNode, nearTargetNode });

        ActionNode moveToTargetNode = new ActionNode(() => SetDestination(fov.visibleTargets[0].position));

        SelectorNode hasWeaponSelector = new SelectorNode(new List<Node>() { attackSequence, moveToTargetNode });

        ActionNode getWeaponOut = new ActionNode(() => controller.EquipWeapon());

        rootNode = new SelectorNode(new List<Node>() { hasWeaponSelector, getWeaponOut });
    }

}
