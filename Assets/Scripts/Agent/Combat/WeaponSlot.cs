using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSlot : MonoBehaviour
{
    public Weapon CurrentlyEquipped { get; private set; }

    public void Equip(Weapon equipment)
    {
        UnEquip();
        if (equipment != null)
        {
            CurrentlyEquipped = equipment;
            CurrentlyEquipped.transform.position = transform.position;
            CurrentlyEquipped.transform.rotation = transform.rotation;
            CurrentlyEquipped.transform.parent = transform;
            CurrentlyEquipped.gameObject.SetActive(true);
        }
    }

    public void UnEquip()
    {
        CurrentlyEquipped?.gameObject.SetActive(false);
        CurrentlyEquipped = null;
    }
}
