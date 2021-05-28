using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AIFighting : NPCState
{
    bool strafeRight;
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
        strafeRight = UnityEngine.Random.value > .5;
        if (UnityEngine.Random.value > .5) // 50/50 chance for momentum attack
        {
            agent.Forwards = true;
            agent.Attack = true;
        }
    }

    Vector3 directionToOpponent;
    float angleToOpponent;
    public override void DuringExecution()
    {
        if (opponent != null)
        {
            directionToOpponent = (opponent.transform.position - transform.position).normalized;
            angleToOpponent = Vector3.SignedAngle(transform.forward, directionToOpponent, Vector3.up);
        }
        base.DuringExecution();
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
            // strafe if opponent blocking
            new SequenceNode(new List<Node>()
            {
                new ConditionNode(() => Node.ConvertToState(opponent.CurrentState.GetType() == typeof(Blocking))),
                new SelectorNode(new List<Node>()
                {
                    // right strafe sequence
                    new SequenceNode(new List<Node>()
                    {
                        new ConditionNode(() => Node.ConvertToState(angleToOpponent <= 0)),
                        new ActionNode(() => agent.Right = true),
                        new ActionNode(() => controller.ChangeLookDirection(controller.Target)),
                    }),
                    // left strafe sequence
                    new SequenceNode(new List<Node>()
                    {
                        new ConditionNode(() => Node.ConvertToState(angleToOpponent > 0)),
                        new ActionNode(() => agent.Left = true),
                        new ActionNode(() => controller.ChangeLookDirection(controller.Target)),
                    })
                }),
            }),
        });
    }

}
