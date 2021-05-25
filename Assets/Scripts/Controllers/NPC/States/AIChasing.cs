using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIChasing : NPCState
{
    public AIChasing(GameObject gameObject) : base(gameObject)
    {
        transitionsTo.Add(new Transition(typeof(AISearching), Not(PlayerInSight)));
        transitionsTo.Add(new Transition(typeof(AIEquipping), Not(() => weapons.HasWeaponEquipped())));
        transitionsTo.Add(new Transition(typeof(AIFighting), () => controller.NearTarget(controller.attackRadius - 1), () => weapons.HasWeaponEquipped()));
    }

    public override void AfterExecution()
    {

    }

    public override void BeforeExecution()
    {
        Debug.Log("NPC Chasing");
        controller.SetDestination(transform.position);
    }

    protected override void CreateTree()
    {
        rootNode = new SelectorNode(new List<Node>()
        {
            new SequenceNode(new List<Node>()
            {
                new ConditionNode(() => Node.ConvertToState(controller.Target != null)), // has target
                new ActionNode(() => controller.SetDestination(controller.Target.position)), // set target
                new ActionNode(() => agent.Forwards = true), // move to target
            }),

        });
    }
}
