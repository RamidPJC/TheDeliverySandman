using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class FSM
{
    private FSMState currentState { get; set; }

    private Dictionary<Type, FSMState> states = new Dictionary<Type, FSMState>();

    public void AddState(FSMState state)
    {
        states.Add(state.GetType(), state);
    }

    public void SetState<T>() where T : FSMState
    {
        var type = typeof(T);

        if (currentState?.GetType() == type)
        {
            return;
        }

        if (states.TryGetValue(type, out var newState))
        {
            currentState?.Exit();
            currentState = newState;
            currentState.Enter();
        }
    }

    public void Update()
    {
        currentState?.Update();
    }

    public void FixedUpdate()
    {
        currentState?.FixedUpdate();
    }
}
