using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIEquipping : NPCState
{
    private bool primaryDone = false;
    private bool secondaryDone = false;

    private bool primaryChosen;
    private bool secondaryChosen;
    private int primaryChoice;
    private int secondaryChoice;
    private List<Weapon> availableWeapons = new List<Weapon>();

    public AIEquipping(GameObject gameObject) : base(gameObject)
    {
        transitionsTo.Add(new Transition(typeof(AIChasing), () => primaryDone && secondaryDone));
    }

    public override void AfterExecution()
    {

    }

    public override void BeforeExecution()
    {
        Debug.Log("AI Equipping Weapon");
        primaryDone = false;
        secondaryDone = false;
        primaryChosen = false;
        secondaryChosen = false;
        primaryChoice = -1;
        secondaryChoice = -1;
        availableWeapons = weapons.CarriedWeapons;
        controller.SetDestination(transform.position, false);
    }

    protected override void CreateTree()
    {
        ActionNode finishPrimary = new ActionNode(() => primaryDone = true);
        ActionNode equipPrimary = new ActionNode(() => controller.EquipWeapon(primaryChoice));
        ConditionNode hasPrimaryChosen = new ConditionNode(() => Node.ConvertToState(primaryChosen));
        SequenceNode primaryEquipSequence = new SequenceNode(new List<Node>() {hasPrimaryChosen,  equipPrimary, finishPrimary});

        ActionNode finishSecondary = new ActionNode(() => secondaryDone = true);
        ActionNode equipSecondary = new ActionNode(() => controller.EquipWeapon(secondaryChoice));
        ConditionNode hasSecondaryChosen = new ConditionNode(() => Node.ConvertToState(secondaryChosen));
        SequenceNode secondaryEquipSequence = new SequenceNode(new List<Node>() { hasSecondaryChosen, equipSecondary, finishSecondary });


        ActionNode choosePrimary = new ActionNode(() => primaryChosen = true);
        ActionNode chooseSecondary = new ActionNode(() => secondaryChosen = true);

        ActionNode chooseTwoHanded = new ActionNode(() => primaryChoice = IndexOfWeaponType(WeaponStance.TwoHanded));
        ConditionNode hasTwoHanded = new ConditionNode(() => Node.ConvertToState(IndexOfWeaponType(WeaponStance.TwoHanded) != -1));
        SequenceNode chooseTwoHandedSequence = new SequenceNode(new List<Node>()
        { new InverterNode(hasPrimaryChosen), hasTwoHanded, chooseTwoHanded, choosePrimary, chooseSecondary });

        ActionNode chooseOneHanded = new ActionNode(() => primaryChoice = IndexOfWeaponType(WeaponStance.OneHandedShield));
        ConditionNode hasOneHanded = new ConditionNode(() => Node.ConvertToState(IndexOfWeaponType(WeaponStance.OneHandedShield) != -1));
        SequenceNode chooseOneHandedSequence = new SequenceNode(new List<Node>()
        {new InverterNode(hasPrimaryChosen), hasOneHanded, chooseOneHanded, choosePrimary });

        ActionNode chooseShield = new ActionNode(() => secondaryChoice = IndexOfWeaponType(WeaponStance.Shield));
        ConditionNode hasShield = new ConditionNode(() => Node.ConvertToState(IndexOfWeaponType(WeaponStance.Shield) != -1));
        SequenceNode chooseShieldSequence = new SequenceNode(new List<Node>()
        { hasShield, chooseShield, chooseSecondary });

        SelectorNode shieldSelector = new SelectorNode(new List<Node>() { chooseShieldSequence, chooseSecondary });

        SelectorNode chooseWeaponsSelector = new SelectorNode(new List<Node>() { chooseTwoHandedSequence, chooseOneHandedSequence, shieldSelector });

        rootNode = new SelectorNode(new List<Node>() { primaryEquipSequence, secondaryEquipSequence, chooseWeaponsSelector });
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

}
