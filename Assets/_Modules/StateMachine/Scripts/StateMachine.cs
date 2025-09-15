using System;
using System.Collections.Generic;
using Alvin.TowerDefense.Combat;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    private State currentState;

    // Dictionary key-value. Dung de truy xuat du lieu theo id
    private readonly Dictionary<Type, State> stateLookup = new Dictionary<Type, State>();

    public void Initialize()
    {
        State[] states = GetComponents<State>();

        foreach (State state in states)
        {
            stateLookup.Add(state.GetType(), state);
        }
    }

    private State GetState<T>() where T : State
    {
        Type stateType = typeof(T);
        return GetState(stateType);
    }

    private State GetState(Type stateType)
    {
        //truyen vao type de lay State trong dictionary
        if (stateLookup.ContainsKey(stateType))
        {
            return stateLookup[stateType];
        }

        Debug.LogError($"State not found: {stateType}");
        return null;
    }

    void Update()
    {
        if (currentState != null)
        {
            currentState.OnStateUpdate();
        }
    }

    public void ChangeState(Type stateType)
    {
        if (currentState != null)
        {
            currentState.OnStateExit();
        }

        State newState = GetState(stateType);
        currentState = newState;
        currentState.OnStateEnter(this);
    }

    
    /// <summary>
    /// Type safe
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public void ChangeState<T>() where T : State
    {
        Type stateType = typeof(T);
        ChangeState(stateType);
    }
}
