using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftSlashing : AttackingState
{
    public LeftSlashing(GameObject gameObject) : base(gameObject)
    {
        animationHash = Animator.StringToHash("LeftSlashing");
    }
}
