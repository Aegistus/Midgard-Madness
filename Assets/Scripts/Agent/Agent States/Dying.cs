using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Dying : AgentState
{
    int animation = 0;
    int animationVariantsTotal = 5;

    public Dying(GameObject gameObject) : base(gameObject)
    {
        animationHash = Animator.StringToHash("Dying");
    }

    public override void AfterExecution()
    {

    }

    public override void BeforeExecution()
    {
        animation = Random.Range(0, animationVariantsTotal);
        anim.SetInteger(animationHash, animation);
    }

    public override void DuringExecution()
    {
        self.SetHorizontalVelocity(Vector3.zero);
    }
}
