using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIEquipping : NPCState
{
    private bool primaryChosen;
    private bool secondaryChosen;
    private int primaryChoice;
    private int secondaryChoice;
    private List<Weapon> availableWeapons = new List<Weapon>();

    public AIEquipping(GameObject gameObject) : base(gameObject)
    {

    }

    public override void AfterExecution()
    {

    }

    public override void BeforeExecution()
    {
        primaryChosen = false;
        secondaryChosen = false;
        primaryChoice = -1;
        secondaryChoice = -1;
        availableWeapons = weapons.CarriedWeapons;
    }

    protected override void CreateTree()
    {
        ActionNode equipPrimary = new ActionNode(() => controller.EquipWeapon(primaryChoice));
        ActionNode equipSecondary = new ActionNode(() => controller.EquipWeapon(secondaryChoice));
        ActionNode hasSecondaryChosen = new ActionNode(() => primaryChosen ? NodeState.SUCCESS : NodeState.FAILURE);
        ActionNode hasPrimaryChosen = new ActionNode(() => secondaryChosen ? NodeState.SUCCESS : NodeState.FAILURE);
        SequenceNode equipSequence = new SequenceNode(new List<Node>() {hasPrimaryChosen, hasSecondaryChosen, equipPrimary, equipSecondary});

        ActionNode choosePrimary = new ActionNode(() => ChoosePrimary());
        ActionNode chooseSecondary = new ActionNode(() => ChooseSecondary());

        ActionNode chooseTwoHanded = new ActionNode(() => SetPrimaryChoice(IndexOfWeaponType(WeaponStance.TwoHanded)));
        ActionNode hasTwoHanded = new ActionNode(() => IndexOfWeaponType(WeaponStance.TwoHanded) != -1 ? NodeState.SUCCESS : NodeState.FAILURE);
        SequenceNode chooseTwoHandedSequence = new SequenceNode(new List<Node>() { hasTwoHanded, chooseTwoHanded, choosePrimary, chooseSecondary });

        ActionNode chooseOneHanded = new ActionNode(() => SetPrimaryChoice(IndexOfWeaponType(WeaponStance.OneHandedShield)));
        ActionNode hasOneHanded = new ActionNode(() => IndexOfWeaponType(WeaponStance.OneHandedShield) != -1 ? NodeState.SUCCESS : NodeState.FAILURE);
        SequenceNode chooseOneHandedSequence = new SequenceNode(new List<Node>() { hasOneHanded, chooseOneHanded, choosePrimary });

        ActionNode chooseShield = new ActionNode(() => SetSecondaryChoice(IndexOfWeaponType(WeaponStance.Shield)));
        ActionNode hasShield = new ActionNode(() => IndexOfWeaponType(WeaponStance.Shield) != -1 ? NodeState.SUCCESS : NodeState.FAILURE);
        SequenceNode chooseShieldSequence = new SequenceNode(new List<Node>() { hasShield, chooseShield, chooseSecondary });

        SelectorNode shieldSelector = new SelectorNode(new List<Node>() { chooseShieldSequence, chooseSecondary });

        SelectorNode chooseWeaponsSelector = new SelectorNode(new List<Node>() { chooseTwoHandedSequence, chooseOneHandedSequence, shieldSelector });

        rootNode = new SelectorNode(new List<Node>() { equipSequence, chooseWeaponsSelector });
    }

    protected NodeState ChoosePrimary()
    {
        primaryChosen = true;
        return NodeState.SUCCESS;
    }

    protected NodeState ChooseSecondary()
    {
        secondaryChosen = true;
        return NodeState.SUCCESS;
    }

    protected int IndexOfWeaponType(WeaponStance stance)
    {
        for (int i = 0; i < availableWeapons.Count; i++)
        {
            if (availableWeapons[i].stats.stance == stance)
            {
                return i;
            }
        }
        return -1;
    }

    protected NodeState SetPrimaryChoice(int choice)
    {
        primaryChoice = choice;
        return NodeState.SUCCESS;
    }

    protected NodeState SetSecondaryChoice(int choice)
    {
        secondaryChoice = choice;
        return NodeState.SUCCESS;
    }

}
