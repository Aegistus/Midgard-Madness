using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIFighting : NPCState
{
    public AIFighting(GameObject gameObject) : base(gameObject)
    {
        transitionsTo.Add(new Transition(typeof(AIWandering), Not(PlayerInSight)));
        transitionsTo.Add(new Transition(typeof(AIChasing), PlayerInSight, Not(() => controller.NearTarget(controller.attackRadius))));
    }

    public override void AfterExecution()
    {

    }

    public override void BeforeExecution()
    {
        Debug.Log("NPC Fighting");
        controller.SetDestination(transform.position, false);
        if (Random.value > .5) // 50/50 chance
        {
            controller.MomentumAttackEnemy();
        }
    }

    protected override void CreateTree()
    {
        //ActionNode momentumAttack = new ActionNode(() => controller.MomentumAttackEnemy());
        //ActionNode isRunning = new ActionNode(IsRunning);
        //SequenceNode momentumAttackSequence = new SequenceNode(new List<Node> { isRunning, momentumAttack });

        ActionNode attackTarget = new ActionNode(() => controller.AttackEnemy());
        WaitNode delay = new WaitNode(attackTarget, 2f);
        SequenceNode attackSequence = new SequenceNode(new List<Node>() { delay, attackTarget });

        ActionNode lookAtTarget = new ActionNode(() => controller.LookAt(controller.Target));

        rootNode = new SelectorNode(new List<Node>() { attackSequence, lookAtTarget });
    }

}
