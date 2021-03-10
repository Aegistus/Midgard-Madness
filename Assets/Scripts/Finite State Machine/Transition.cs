using System;

public class Transition
{
    /// <summary>
    /// State this transition will go to if Condition == true.
    /// </summary>
    public Type ToState { get; }
    /// <summary>
    /// The condition that will activate this transition.
    /// </summary>
    public Func<bool> Condition { get; }

    /// <summary>
    /// Can this state transition to itself?
    /// </summary>
    public bool CanTransitionToSelf { get; set; } = false;

    public Transition(Type toState, Func<bool> condition)
    {
        ToState = toState;
        Condition = condition;
    }

    public Transition(Type toState, Func<bool> condition1, Func<bool> condition2)
    {
        ToState = toState;
        Condition = () => condition1() && condition2();
    }

    public Transition(Type toState, Func<bool> condition1, Func<bool> condition2, Func<bool> condition3)
    {
        ToState = toState;
        Condition = () => condition1() && condition2() && condition3();
    }

    public Transition(Type toState, Func<bool> condition1, Func<bool> condition2, Func<bool> condition3, Func<bool> condition4)
    {
        ToState = toState;
        Condition = () => condition1() && condition2() && condition3() && condition4();
    }
}
