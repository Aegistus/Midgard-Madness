using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blocking : AgentState
{
    //private float blockTime = 1f;
    //private float shieldBlockTime = 4f;
    private float timer = 0;

    public Blocking(GameObject gameObject) : base(gameObject)
    {
        animationHash = Animator.StringToHash("Blocking");
        transitionsTo.Add(new Transition(typeof(Idling), Not(Block)));
        //transitionsTo.Add(new Transition(typeof(Idling), ShieldEquipped, () => timer >= shieldBlockTime));
        //transitionsTo.Add(new Transition(typeof(Idling), Not(ShieldEquipped), () => timer >= blockTime));
    }

    public override void AfterExecution()
    {
        anim.SetBool(animationHash, false);
    }

    public override void BeforeExecution()
    {
        Debug.Log("Blocking");
        anim.SetBool(animationHash, true);
        self.SetHorizontalVelocity(Vector3.zero);
        timer = 0;
    }

    public override void DuringExecution()
    {
        timer += Time.deltaTime;
    }
}
