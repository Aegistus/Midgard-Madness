using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fighting : NPCState
{
    public Fighting(GameObject gameObject) : base(gameObject)
    {
        transitionsTo.Add(new Transition(typeof(Wandering), Not(PlayerInSight)));
        transitionsTo.Add(new Transition(typeof(Chasing), PlayerInSight, () => AtDestination(controller.attackRadius) != NodeState.SUCCESS));
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
        ActionNode attackTarget = new ActionNode(() => controller.AttackEnemy());

        rootNode = new SelectorNode(new List<Node>() { attackTarget });
    }

}
