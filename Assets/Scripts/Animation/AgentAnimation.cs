using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AnimEventType
{
    Start, Finish, WeaponSound, DamageStart, DamageEnd, Footstep, ChainStart
}

/**
 * Place on the agent's model next to the Animator
 */
[RequireComponent(typeof(Animator))]
public class AgentAnimation : MonoBehaviour
{
    public event Action<AnimEventType> OnAnimationEvent;

    Agent agent;
    Animator anim;
    int upperBodyLayerIndex;
    MultiDictionary<Type, int> upperBodyStates = new MultiDictionary<Type, int>();
    int fullBodyLayerIndex;
    MultiDictionary<Type, int> fullBodyStates = new MultiDictionary<Type, int>();

    private void Start()
    {
        agent = GetComponentInParent<Agent>();
        anim = GetComponent<Animator>();
        fullBodyLayerIndex = anim.GetLayerIndex("Full Body");
        fullBodyStates = new MultiDictionary<Type, int>()
        {
            {typeof(Running), Animator.StringToHash("Running") },
            {typeof(Jumping), Animator.StringToHash("Jumping")},
            {typeof(Falling), Animator.StringToHash("Falling") },
            {typeof(Crouching), Animator.StringToHash("Crouching") },
        };
    }

    private void ChangeUpperBodyAnimation(Type newState)
    {
        if (newState == typeof(Idling))
        {
            anim.SetLayerWeight(upperBodyLayerIndex, 0);
        }
        else if (upperBodyStates.ContainsKey(newState))
        {
            anim.SetLayerWeight(upperBodyLayerIndex, 1);
            anim.CrossFade(upperBodyStates[newState], .05f, upperBodyLayerIndex);
        }
        else
        {
            Debug.LogWarning("No Upper Body Animation Found For Current Agent State");
        }
    }

    private void ChangeFullBodyAnimation(Type newState)
    {
        if (fullBodyStates.ContainsKey(newState))
        {
            anim.speed = 1;
            anim.CrossFade(fullBodyStates[newState], .05f, fullBodyLayerIndex);
        }
        else
        {
            Debug.LogWarning("No Full Body Animation Found For Current Agent State");
        }
    }

    public void CallAnimationEvent(AnimEventType eventType)
    {
        OnAnimationEvent?.Invoke(eventType);
    }

}
