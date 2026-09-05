using Unity.VisualScripting;
using UnityEngine;

public class ThiefFSMStateOnFoot : ThiefFSMStateMovement
{
    private float distanceToTargetForDash;
    private float sneakSpeed;

    public ThiefFSMStateOnFoot(FSM fsm, Transform transform, Rigidbody rb, Animator animator, ThiefAI thiefAI, 
        GroundChecker groundChecker, float distanceToTargetForDash, float sneakSpeed) : base(fsm, transform, rb, animator, thiefAI, groundChecker)
    {
        this.distanceToTargetForDash = distanceToTargetForDash;
        this.sneakSpeed = sneakSpeed;
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void FixedUpdate()
    {
        if (thiefAI.currentTarget)
        {
            Vector3 dir = thiefAI.currentTarget.position - transform.position;
            LookToDir(dir);

            if (dir.magnitude < distanceToTargetForDash)
            {
                SneakToTarget(dir);
            }
            else
            {
                fsm.SetState<ThiefFSMStateJump>();
            }
        }

        if (!groundChecker.CheckGround())
        {
            fsm.SetState<ThiefFSMStateFall>();
        }

        SetBlendTreeMotion();
    }

    private void LookToDir(Vector3 dir)
    {
        dir.y = 0;
        transform.rotation = Quaternion.LookRotation(dir);
    }

    private void SneakToTarget(Vector3 vectorToTarget)
    {
        Vector3 velocity = rb.linearVelocity;
        velocity.x = vectorToTarget.normalized.x * sneakSpeed;
        velocity.z = vectorToTarget.normalized.z * sneakSpeed;
        rb.linearVelocity = velocity;
    }

    private void SetBlendTreeMotion()
    {
        Vector2 velocity = rb.linearVelocity.FromXZ();
        float axis = velocity.magnitude > 1 ? velocity.normalized.magnitude : velocity.magnitude;
        animator.SetFloat("Vertical", axis);
    }
}
