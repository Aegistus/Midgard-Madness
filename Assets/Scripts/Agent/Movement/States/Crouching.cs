using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crouching : MovementState
{
    float crouchHeight = .5f;

    public Crouching(GameObject gameObject) : base(gameObject)
    {
        animationHash = Animator.StringToHash("Crouching");
        transitionsTo.Add(new Transition(typeof(Idling), Not(Crouch)));
    }

    public override void AfterExecution()
    {
        anim.SetBool(animationHash, false);
        //transform.position += Vector3.down * crouchHeight;
    }

    public override void BeforeExecution()
    {
        Debug.Log("Crouching");
        anim.SetBool(animationHash, true);
        movement.SetHorizontalVelocity(Vector3.zero);
        //transform.position -= Vector3.down * crouchHeight;
    }

    public override void DuringExecution()
    {

    }
}
