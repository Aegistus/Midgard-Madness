using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stabbing : AttackingState
{
    public Stabbing(GameObject gameObject) : base(gameObject)
    {
        animationHash = Animator.StringToHash("Stabbing");
    }

}
