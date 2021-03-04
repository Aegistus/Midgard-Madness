using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stabbing : MeleeAttacking
{
    public Stabbing(GameObject gameObject) : base(gameObject)
    {
        animationHash = Animator.StringToHash("Stabbing");
    }

}
