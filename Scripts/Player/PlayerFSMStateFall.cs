using UnityEngine;

public class PlayerFSMStateFall : PlayerFSMStateMovement
{
    private GroundChecker groundChecker;

    public PlayerFSMStateFall (FSM fsm, Controller controller, Animator animator, GroundChecker groundChecker) : base(fsm, controller, animator)
    {
        this.groundChecker = groundChecker;
    }

    public override void FixedUpdate()
    {
        if (!groundChecker.CheckGround())
        {
            LandOnSurface();
        }
        else
        {
            fsm.SetState<PlayerFSMStateOnFoot>();
        }
    }

    private void LandOnSurface()
    {
        Vector3 landingForce = rb.linearVelocity;
        landingForce.y += Physics.gravity.y * 1.5f * Time.fixedDeltaTime;
        rb.linearVelocity = landingForce;
    }
}
