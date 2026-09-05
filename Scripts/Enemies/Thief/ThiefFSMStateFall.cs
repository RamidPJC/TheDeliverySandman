using UnityEngine;

public class ThiefFSMStateFall : ThiefFSMStateMovement
{

    public ThiefFSMStateFall(FSM fsm, Transform transform, Rigidbody rb, Animator animator, ThiefAI thiefAI,
        GroundChecker groundChecker) : base(fsm, transform, rb, animator, thiefAI, groundChecker) { }

    public override void Enter()
    {
        base.Enter();
        animator.SetBool("isFalling", true);
    }

    public override void FixedUpdate()
    {
        if (groundChecker.CheckGround())
        {
            fsm.SetState<ThiefFSMStateOnFoot>();
        }
    }

    public override void Exit()
    {
        animator.SetBool("isFalling", false);
    }
}
