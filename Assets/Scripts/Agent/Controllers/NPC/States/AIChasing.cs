using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIChasing : NPCState
{
    public AIChasing(GameObject gameObject) : base(gameObject)
    {
        transitionsTo.Add(new Transition(typeof(AIWandering), Not(PlayerInSight)));
        transitionsTo.Add(new Transition(typeof(AIEquipping), Not(() => weapons.HasWeaponEquipped())));
        transitionsTo.Add(new Transition(typeof(AIFighting), () => controller.NearTarget(controller.attackRadius), () => weapons.HasWeaponEquipped()));
    }

    public override void AfterExecution()
    {

    }

    public override void BeforeExecution()
    {
        Debug.Log("NPC Chasing");
        controller.Target = fov.visibleTargets[0];
        controller.SetDestination(transform.position, true);
    }

    protected override void CreateTree()
    {
        InverterNode notNearTarget = new InverterNode(new ConditionNode(() => Node.ConvertToState(controller.NearTarget(controller.attackRadius))));
        ActionNode setTarget = new ActionNode(() => controller.SetDestination(controller.Target.position, true));
        ActionNode moveToTarget = new ActionNode(() => controller.MoveToDestination(true));
        SequenceNode chaseSequence = new SequenceNode(new List<Node>() { setTarget, moveToTarget });

        rootNode = new SelectorNode(new List<Node>() { chaseSequence });
    }
}
