using UnityEngine;

public class PlayerFSMStateInVehicle : FSMState
{
    private Driver driver;
    private Animator animator;

    public PlayerFSMStateInVehicle(FSM fsm, Driver driver, Animator animator) : base(fsm)
    {
        this.driver = driver;
        this.animator = animator;
    }

    public override void Enter()
    {
        driver.OnExitedHandler += ExitVehicle;
        animator.SetBool("isInVehicle", true);
    }

    public override void Exit()
    {
        driver.OnExitedHandler -= ExitVehicle;
    }

    private void ExitVehicle()
    {
        animator.SetBool("isInVehicle", false);
        fsm.SetState<PlayerFSMStateOnFoot>();
    }
}
