using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipWeaponNode : Node
{
    private AgentWeapons weapons;
    private AgentController controller;

    public EquipWeaponNode(AgentWeapons weapons, AgentController controller)
    {
        this.weapons = weapons;
        this.controller = controller;
    }

    public override NodeState Evaluate()
    {
        if (weapons.primarySlot.CurrentlyEquipped != null || weapons.secondarySlot.CurrentlyEquipped != null)
        {
            return NodeState.SUCCESS;
        }
        else
        {
            controller.Equipping = true;
            controller.WeaponNumKey = 1;
            if (weapons.primarySlot.CurrentlyEquipped != null || weapons.secondarySlot.CurrentlyEquipped != null)
            {
                return NodeState.SUCCESS;
            }
            else
            {
                return NodeState.FAILURE;
            }
        }
    }
}
