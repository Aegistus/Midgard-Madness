using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCController : AgentController
{
    private FieldOfView fov;
    private AgentWeapons weapons;
    private AgentMovement movement;
    private AgentCombat combat;
    private NavMeshAgent navAgent;

    private SelectorNode rootNode;


    private void Start()
    {
        fov = GetComponent<FieldOfView>();
        weapons = GetComponent<AgentWeapons>();
        movement = GetComponent<AgentMovement>();
        combat = GetComponent<AgentCombat>();
        navAgent = GetComponent<NavMeshAgent>();
        CreateTree();
    }

    private void CreateTree()
    {
        AttackNode attackNode = new AttackNode(this);
        MoveToTargetNode moveNode = new MoveToTargetNode(transform, this, navAgent, fov);
        EquipWeaponNode equipNode = new EquipWeaponNode(weapons, this);
        IsPlayerVisibleNode playerVisible = new IsPlayerVisibleNode(fov);
        SequenceNode attackBranch = new SequenceNode(new List<Node>() { playerVisible, equipNode, moveNode, attackNode });

        rootNode = new SelectorNode(new List<Node>() { attackBranch });
    }

    private void Update()
    {
        Attack = false;
        Block = false;
        Forwards = false;
        Backwards = false;
        Left = false;
        Right = false;
        Jump = false;
        Crouch = false;
        Run = false;
        Equipping = false;
        rootNode.Evaluate();
    }
}
