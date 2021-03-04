using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightSlashing : MeleeAttacking
{
    public RightSlashing(GameObject gameObject) : base(gameObject)
    {
        animationHash = Animator.StringToHash("RightSlashing");
    }
}
