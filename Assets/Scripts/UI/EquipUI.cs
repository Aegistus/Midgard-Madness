using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipUI : MonoBehaviour
{
    public List<Image> panels;
    public Color equippedColor;
    public Color notEquippedColor;

    private AgentWeapons playerWeapons;

    private void Start()
    {
        playerWeapons = GameObject.FindGameObjectWithTag("Player").GetComponent<AgentWeapons>();
        playerWeapons.OnEquippedChange += UpdateWeaponEquipState;
        UpdateWeaponEquipState();
    }

    private void UpdateWeaponEquipState()
    {
        Weapon primary = playerWeapons.primarySlot.CurrentlyEquipped;
        Weapon secondary = playerWeapons.secondarySlot.CurrentlyEquipped;
        int primaryIndex = -1;
        int secondaryIndex = -1;
        for (int i = 0; i < playerWeapons.CarriedWeapons.Count; i++)
        {
            if (playerWeapons.CarriedWeapons[i] == primary)
            {
                primaryIndex = i;
            }
            if (playerWeapons.CarriedWeapons[i] == secondary)
            {
                secondaryIndex = i;
            }
        }
        for (int i = 0; i < panels.Count; i++)
        {
            if (i == primaryIndex || i == secondaryIndex)
            {
                panels[i].color = equippedColor;
            }
            else
            {
                panels[i].color = notEquippedColor;
            }
        }
    }
}
