using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Dying : AgentState
{
    int animation = 0;
    int animationTotal = 6;

    public Dying(GameObject gameObject) : base(gameObject)
    {
        animationHash = Animator.StringToHash("Dying");
    }

    public override void AfterExecution()
    {

    }

    public override void BeforeExecution()
    {
        animation = Random.Range(0, animationTotal);
        anim.SetInteger(animationHash, animation);
    }

    public override void DuringExecution()
    {
        movement.SetHorizontalVelocity(Vector3.zero);
    }
}
