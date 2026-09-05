using System.Collections;
using UnityEngine;
using System;

public class ThiefAI : EnemyAI
{
    public Transform entryPoint;

    //dash
    [SerializeField] private float distanceToTargetForDash;
    private bool isPerformingJump;
    private bool isFloating;

    private const float DEFAULT_XZ_MAGNITUDE = 12.98f;
    private const float DEFAULT_FORCE = 11.7f;
    private const float DEFAULT_Y = 7;

    //sneak
    [SerializeField] private float sneakSpeed;

    //fsm
    private FSM fsm;

    //stealing
    [SerializeField] private ThiefInteractionBrain brain;
    private bool isReturningToBaseWithStolen;
    public event Action OnReturnedToBaseWithStolenHandler;

    protected new void Start()
    {
        base.Start();

        SetEntryPoint();

        brain.OnCatchedStealableHandler += SwitchTargetToEntryPointIfStolen;

        fsm = new FSM();
        fsm.AddState(new ThiefFSMStateOnFoot(fsm, transform, rb, animator, this, groundChecker, distanceToTargetForDash, sneakSpeed));
        fsm.AddState(new ThiefFSMStateJump(fsm, transform, rb, animator, this, groundChecker, DEFAULT_XZ_MAGNITUDE, DEFAULT_FORCE, DEFAULT_Y));
        fsm.AddState(new ThiefFSMStateFall(fsm, transform, rb, animator, this, groundChecker));
        fsm.SetState<ThiefFSMStateOnFoot>();
    }

    private void FixedUpdate()
    {
        fsm.FixedUpdate();

        if (isReturningToBaseWithStolen)
        {
            float distance = (currentTarget.position - transform.position).magnitude;
            if (distance <= 5)
            {
                isReturningToBaseWithStolen = false;
                OnReturnedToBaseWithStolenHandler?.Invoke();
            }
        }
    }

    protected override void OnMovingObjectDetected(bool entered, MovingObject target)
    {
        base.OnMovingObjectDetected(entered, target);
        if (!entered)
        {
            currentTarget = entryPoint;
        }
    }

    private void SetEntryPoint()
    {
        entryPoint = new GameObject().transform;
        entryPoint.SetParent(detectionArea.transform);
        entryPoint.position = transform.position;
        entryPoint.name = "Entry Point For " + transform.name;
    }

    private void SwitchTargetToEntryPointIfStolen()
    {
        currentTarget = entryPoint;
        isReturningToBaseWithStolen = true;
    }

    protected override void OnDied()
    {
        base.OnDied();
        Destroy(stats);
        Destroy(brain);
        Destroy(this);
    }

    private void OnDestroy()
    {
        brain.OnCatchedStealableHandler -= SwitchTargetToEntryPointIfStolen;
    }
}
