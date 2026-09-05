using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class PlayerFSMStateOnFoot : PlayerFSMStateMovement
{
    private Transform transform;
    private Driver driver;
    private GroundChecker groundChecker;
    private PlayerStats playerStats;

    private float speed;
    private float additionalSpeed;

    private float xAxis;
    private float zAxis;

    private float currentSpeed;

    public PlayerFSMStateOnFoot (FSM fsm, Transform transform, Controller controller, Driver driver, Animator animator,
        GroundChecker groundChecker, PlayerStats playerStats, float speed, float additionalSpeed) : base(fsm, controller, animator)
    {
        this.transform = transform;
        this.driver = driver;
        this.groundChecker = groundChecker;
        this.playerStats = playerStats;
        this.speed = speed;
        this.additionalSpeed = additionalSpeed;
    }

    public override void Enter()
    {
        driver.OnEnteredVehicle += EnterInVehicleState;

        base.Enter();
    }

    public override void Exit()
    {
        driver.OnEnteredVehicle -= EnterInVehicleState;
    }

    public override void Update()
    {
        xAxis = Input.GetAxis("Horizontal");
        zAxis = Input.GetAxis("Vertical");

        currentSpeed = speed;

        if (Input.GetButton("Run"))
        {
            if (playerStats.CanRun())
            {
                playerStats.SetRunningState(true);
                currentSpeed += additionalSpeed;
                SetBlendTreeMotion(xAxis * 2, zAxis * 2);
            }
            else
            {
                SetBlendTreeMotion(xAxis, zAxis);
            }
        }
        else
        {
            playerStats.SetRunningState(false);
            SetBlendTreeMotion(xAxis, zAxis);
        }

        if (Input.GetButtonDown("Jump"))
        {
            fsm.SetState<PlayerFSMStateJump>();
        }
    }

    public override void FixedUpdate()
    {
        Move();

        if (!groundChecker.CheckGround())
        {
            fsm.SetState<PlayerFSMStateFall>();
        }
    }

    private void Move()
    {
        Vector3 move = (transform.forward * zAxis + transform.right * xAxis) * currentSpeed;

        Vector3 velocity = rb.linearVelocity;
        velocity.x = move.x;
        velocity.z = move.z;

        rb.linearVelocity = velocity;
    }

    private void SetBlendTreeMotion(float x, float z)
    {
        float hor = Mathf.Lerp(animator.GetFloat("Horizontal"), x, currentSpeed * Time.deltaTime);
        float ver = Mathf.Lerp(animator.GetFloat("Vertical"), z, currentSpeed * Time.deltaTime);

        animator.SetFloat("Horizontal", hor);
        animator.SetFloat("Vertical", ver);
    }

    private void EnterInVehicleState()
    {
        fsm.SetState<PlayerFSMStateInVehicle>();
    }
}
