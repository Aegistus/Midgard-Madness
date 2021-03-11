using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIFighting : NPCState
{
    public AIFighting(GameObject gameObject) : base(gameObject)
    {
        transitionsTo.Add(new Transition(typeof(AIWandering), Not(PlayerInSight)));
        transitionsTo.Add(new Transition(typeof(AIChasing), PlayerInSight, () => controller.AtDestination(controller.attackRadius)));
    }

    public override void AfterExecution()
    {

    }

    public override void BeforeExecution()
    {
        Debug.Log("NPC Fighting");
        controller.MomentumAttackEnemy();

    }

    protected override void CreateTree()
    {
        //ActionNode momentumAttack = new ActionNode(() => controller.MomentumAttackEnemy());
        //ActionNode isRunning = new ActionNode(IsRunning);
        //SequenceNode momentumAttackSequence = new SequenceNode(new List<Node> { isRunning, momentumAttack });

        ActionNode attackTarget = new ActionNode(() => controller.AttackEnemy());
        WaitNode delay = new WaitNode(attackTarget, 2f);
        SequenceNode attackSequence = new SequenceNode(new List<Node>() { delay, attackTarget });

        rootNode = new SelectorNode(new List<Node>() { attackSequence });
    }

}
