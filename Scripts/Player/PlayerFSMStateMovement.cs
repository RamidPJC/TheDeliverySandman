using UnityEngine;

public class PlayerFSMStateMovement : FSMState
{
    protected Controller controller;
    protected Animator animator;
    protected Rigidbody rb;

    public PlayerFSMStateMovement(FSM fsm, Controller controller, Animator animator) : base(fsm)
    {
        this.controller = controller;
        this.animator = animator;
    }

    public override void Enter()
    {
        if (!rb)
        {
            rb = controller.GetComponent<Rigidbody>();
        }
    }
}
