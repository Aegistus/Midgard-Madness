using System;
using System.Collections.Generic;
using System.Linq;

public class StateMachine
{
    protected Dictionary<Type, State> availableStates;
    protected Type defaultState;
    public State CurrentState { get; protected set; }
    public event Action<State> OnStateChange;

    protected List<Transition> anyStateTransitions = new List<Transition>();

    /// <summary> Sets the states that can be used by this StateMachine. Also sets which state is default. </summary>
    public virtual void SetStates(Dictionary<Type, State> states, Type defaultState)
    {
        availableStates = states;
        this.defaultState = defaultState;
        List<State> newStateList = states.Values.ToList();
        for (int i = 0; i < newStateList.Count; i++)
        {
            anyStateTransitions.AddRange(newStateList[i].anyStateTransitions);
        }
        CurrentState = availableStates[defaultState];
    }

    /// <summary> Adds to the available number of states that can be used by this StateMachine. </summary>
    public virtual void AddStates(Dictionary<Type, State> newStates)
    {
        foreach (var newState in newStates)
        {
            AddState(newState.Key, newState.Value);
        }
    }

    /// <summary> Adds one state to the number of available states. </summary>
    public virtual void AddState(Type stateType, State state)
    {
        if (!availableStates.ContainsKey(stateType))
        {
            availableStates.Add(stateType, state);
            anyStateTransitions.AddRange(state.anyStateTransitions);
        }
    }

    /// <summary> Changes the machine's state from the CurrentState to the nextState. </summary>
    public virtual void SwitchToNewState(Type nextState)
    {
        CurrentState.AfterExecution();
        availableStates[nextState].BeforeExecution();
        CurrentState = availableStates[nextState];
        OnStateChange?.Invoke(availableStates[nextState]);
    }

    /// <summary>
    /// Loops through all transitions to check their condition.
    /// </summary>
    /// <returns> State from a fulfilled transition </returns>
    private Type CheckAllTransitions()
    {
        for (int i = 0; i < anyStateTransitions.Count; i++)
        {
            if (anyStateTransitions[i].Condition())
            {
                return anyStateTransitions[i].ToState;
            }
        }
        return CurrentState?.CheckTransitions();
    }

    private Type nextState;
    /// <summary>
    /// Should be placed in an Update loop. Checks for fulfilled transition conditions and executes the current state's DuringExecution().
    /// </summary>
    public void ExecuteState()
    {
        if (CurrentState == null)
        {
            CurrentState = availableStates[defaultState];
            CurrentState.BeforeExecution();
        }

        nextState = CheckAllTransitions();
        if (nextState != null)
        {
            SwitchToNewState(nextState);
        }
        CurrentState.DuringExecution();
    }

}
