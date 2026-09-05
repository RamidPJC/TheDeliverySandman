using System.Security.Cryptography;
using UnityEngine;

public abstract class FSMState
{
    protected readonly FSM fsm;

    public FSMState (FSM fsm)
    {
        this.fsm = fsm;
    }

    public virtual void Enter() { }
    public virtual void Exit() { }
    public virtual void Update() { }
    public virtual void FixedUpdate() { }
}
