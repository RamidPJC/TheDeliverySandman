using UnityEngine;

public class ThiefFSMStateMovement : FSMState
{
    protected Transform transform;
    protected Rigidbody rb;
    protected Animator animator;
    protected ThiefAI thiefAI;
    protected GroundChecker groundChecker;
    private EnemyArea detectionArea;

    public ThiefFSMStateMovement(FSM fsm, Transform transform, Rigidbody rb, Animator animator, ThiefAI thiefAI, 
        GroundChecker groundChecker) : base(fsm)
    {
        this.transform = transform;
        this.rb = rb;
        this.animator = animator;
        this.thiefAI = thiefAI;
        this.groundChecker = groundChecker;
    }
}
