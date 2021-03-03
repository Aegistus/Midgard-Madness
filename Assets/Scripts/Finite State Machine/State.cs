using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class State
{
    /// <summary>
    /// Name of this state. For UI display and debugging purposes.
    /// </summary>
    public string StateName { get; protected set; }
    public List<Transition> anyStateTransitions = new List<Transition>();
    protected List<Transition> transitionsTo = new List<Transition>();
    protected GameObject gameObject;
    protected Transform transform;

    public State(GameObject gameObject)
    {
        StateName = " ";
        this.gameObject = gameObject;
        transform = gameObject.transform;
    }

    /// <summary>
    /// Called the frame before the state is entered.
    /// </summary>
    public abstract void BeforeExecution();
    /// <summary>
    /// Called every frame while the state is active.
    /// </summary>
    public abstract void DuringExecution();
    /// <summary>
    /// Called the frame after the state is active and before the next state.
    /// </summary>
    public abstract void AfterExecution();

    /// <summary>
    /// Checks this state's transitions for completed ones.
    /// </summary>
    /// <returns>Any transition with a fulfilled condition or null</returns>
    public virtual Type CheckTransitions()
    {
        for (int i = 0; i < transitionsTo.Count; i++)
        {
            if (transitionsTo[i].Condition())
            {
                return transitionsTo[i].ToState;
            }
        }
        return null;
    }

    /// <summary>
    /// Takes a Func-bool- and returns the negated version.
    /// </summary>
    /// <param name="func"></param>
    /// <returns></returns>
    public Func<bool> Not(Func<bool> func)
    {
        return () => !func();
    }

}
