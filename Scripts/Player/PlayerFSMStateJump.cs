using UnityEngine;

public class PlayerFSMStateJump : PlayerFSMStateMovement
{
    private float jumpSpeed;

    public PlayerFSMStateJump(FSM fsm, Controller controller, Animator animator, float jumpSpeed) : base(fsm, controller, animator)
    {
        this.jumpSpeed = jumpSpeed;
    }

    public override void Enter()
    {
        base.Enter();

        Jump();
        animator.SetTrigger("Jump");
    }

    public override void FixedUpdate()
    {
        if (rb.linearVelocity.y <= 0)
        {
            fsm.SetState<PlayerFSMStateFall>();
        }
    }

    private void Jump()
    {
        rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);

        rb.AddForce(new Vector3(0, jumpSpeed * rb.mass, 0), ForceMode.Impulse);
    }
}
