using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AIFighting : NPCState
{
    Agent opponent;
    Func<bool> NotAttacking => Not(() => agent.CurrentState.GetType() != typeof(MeleeAttacking) && agent.CurrentState.GetType() != typeof(RangedAttacking));

    public AIFighting(GameObject gameObject) : base(gameObject)
    {
        transitionsTo.Add(new Transition(typeof(AISearching), NotAttacking, Not(PlayerInSight)));
        transitionsTo.Add(new Transition(typeof(AIEquipping), Not(() => weapons.HasWeaponEquipped())));
        transitionsTo.Add(new Transition(typeof(AIChasing), PlayerInSight, Not(() => controller.NearTarget(controller.attackRadius))));
    }

    public override void AfterExecution()
    {

    }

    public override void BeforeExecution()
    {
        Debug.Log("NPC Fighting");
        controller.SetDestination(transform.position);
        opponent = controller.Target.GetComponentInParent<Agent>();
        if (UnityEngine.Random.value > .5) // 50/50 chance
        {
            agent.Forwards = true;
            agent.Attack = true;
        }
    }

    protected override void CreateTree()
    {
        rootNode = new SelectorNode(new List<Node>()
        {
            // block sequence
            new SequenceNode(new List<Node>()
            {
                new ConditionNode(() => Node.ConvertToState(opponent.CurrentState.GetType() == typeof(MeleeAttacking))),
                new ConditionNode(() => Node.ConvertToState(UnityEngine.Random.value >= .5)),
                new ActionNode(() => agent.Block = true),
                new ActionNode(() => controller.ChangeLookDirection(controller.Target)), // look at target
            }),
            // attack sequence
            new SequenceNode(new List<Node>()
            {
                new WaitNode(new ActionNode(() => agent.Attack = true), UnityEngine.Random.value * controller.attackWaitTime),
                new ActionNode(() => controller.ChangeLookDirection(controller.Target)), // look at target
            }),
        });
    }

}
