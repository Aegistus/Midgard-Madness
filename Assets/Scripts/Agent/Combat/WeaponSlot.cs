using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSlot : MonoBehaviour
{
    public Weapon CurrentlyEquipped { get; private set; }

    public void Equip(Weapon equipment)
    {
        if (equipment != null)
        {
            CurrentlyEquipped = equipment;
            CurrentlyEquipped.transform.position = transform.position;
            CurrentlyEquipped.transform.rotation = transform.rotation;
            CurrentlyEquipped.transform.parent = transform;
            CurrentlyEquipped.gameObject.SetActive(true);
        }
    }

    public Weapon UnEquip()
    {
        Weapon toReturn = CurrentlyEquipped;
        CurrentlyEquipped = null;
        toReturn?.gameObject.SetActive(false);
        return toReturn;
    }
}
