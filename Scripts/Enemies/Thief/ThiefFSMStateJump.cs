using UnityEngine;

public class ThiefFSMStateJump : ThiefFSMStateMovement
{
    private readonly float DEFAULT_XZ_MAGNITUDE;
    private readonly float DEFAULT_FORCE;
    private readonly float DEFAULT_Y;

    public ThiefFSMStateJump(FSM fsm, Transform transform, Rigidbody rb, Animator animator, ThiefAI thiefAI,
    GroundChecker groundChecker, 
    float DEFAULT_XZ_MAGNITUDE, float DEFAULT_FORCE, float DEFAULT_Y) : base(fsm, transform, rb, animator, thiefAI, groundChecker)
    {
        this.DEFAULT_XZ_MAGNITUDE = DEFAULT_XZ_MAGNITUDE;
        this.DEFAULT_FORCE = DEFAULT_FORCE;
        this.DEFAULT_Y = DEFAULT_Y;
    }

    public override void Enter()
    {
        base.Enter();
        JumpToTarget();
        animator.SetTrigger("Jump");
    }

    public override void FixedUpdate()
    {
        if (rb.linearVelocity.y <= 0 && !groundChecker.CheckGround())
        {
            fsm.SetState<ThiefFSMStateFall>();
        }
    }

    private void JumpToTarget()
    {
        //reset current force
        rb.linearVelocity = Vector3.zero;

        //apply new force
        Vector3 vectorToTarget = thiefAI.currentTarget.position - transform.position;

        float magnitude = vectorToTarget.magnitude;
        float force = DEFAULT_FORCE * Mathf.Sqrt(magnitude / DEFAULT_XZ_MAGNITUDE);

        vectorToTarget.y += (magnitude / DEFAULT_XZ_MAGNITUDE) * DEFAULT_Y;

        Vector3 forceVector = vectorToTarget.normalized * force;
        rb.AddForce(forceVector * rb.mass, ForceMode.Impulse);
    }
}
