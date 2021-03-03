using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightSlashing : AttackingState
{
    public RightSlashing(GameObject gameObject) : base(gameObject)
    {
        animationHash = Animator.StringToHash("RightSlashing");
    }
}
