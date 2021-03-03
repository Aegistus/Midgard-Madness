using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    public enum Usage
    {
        Primary, Secondary, Both, Either
    }

    public Usage usage;
}
