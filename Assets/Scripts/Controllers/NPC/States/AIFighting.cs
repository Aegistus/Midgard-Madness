using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AIFighting : NPCState
{
    Func<bool> NotAttacking => Not(() => agent.CurrentState.GetType() != typeof(MeleeAttacking) && agent.CurrentState.GetType() != typeof(RangedAttacking));

    public AIFighting(GameObject gameObject) : base(gameObject)
    {
        transitionsTo.Add(new Transition(typeof(AISearching), NotAttacking, Not(PlayerInSight)));
        transitionsTo.Add(new Transition(typeof(AIChasing), NotAttacking, PlayerInSight, Not(() => controller.NearTarget(controller.attackRadius))));
    }

    public override void AfterExecution()
    {

    }

    public override void BeforeExecution()
    {
        Debug.Log("NPC Fighting");
        controller.SetDestination(transform.position, false);
        if (UnityEngine.Random.value > .5) // 50/50 chance
        {
            controller.MomentumAttackEnemy();
        }
    }

    public override void DuringExecution()
    {
        base.DuringExecution();
        controller.LookAt(controller.Target);
    }

    protected override void CreateTree()
    {
        //ActionNode momentumAttack = new ActionNode(() => controller.MomentumAttackEnemy());
        //ActionNode isRunning = new ActionNode(IsRunning);
        //SequenceNode momentumAttackSequence = new SequenceNode(new List<Node> { isRunning, momentumAttack });

        ActionNode attackTarget = new ActionNode(() => controller.AttackEnemy());
        WaitNode attackDelay = new WaitNode(attackTarget, UnityEngine.Random.value * controller.attackWaitTime);
        SequenceNode attackSequence = new SequenceNode(new List<Node>() { attackDelay, attackTarget });

        ActionNode blockAttack = new ActionNode(() => controller.BlockAttack());
        WaitNode blockDelay = new WaitNode(blockAttack, UnityEngine.Random.value * controller.attackWaitTime);
        SequenceNode blockSequence = new SequenceNode(new List<Node>() { blockDelay, blockAttack });

        rootNode = new SelectorNode(new List<Node>() { attackSequence, blockSequence });
    }

}
