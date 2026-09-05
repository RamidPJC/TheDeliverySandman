using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Controller
{
    private PlayerStats playerStats;

    [SerializeField] private float speed;
    [SerializeField] private float additionalSpeed;

    [SerializeField] private float jumpSpeed;

    [SerializeField] private CameraController cameraController;

    private Driver driver;

    private InteractionSystem interactionSystem;

    private MissionSystem missionSystem;

    private FSM fsm;

    protected new void Start()
    {
        base.Start();
        
        driver = GetComponent<Driver>();

        playerStats = GetComponent<PlayerStats>();

        interactionSystem = GetComponent<InteractionSystem>();

        missionSystem = GetComponent<MissionSystem>();

        fsm = new FSM();
        fsm.AddState(new PlayerFSMStateOnFoot(fsm, transform, this, driver, animator, groundChecker, playerStats, speed, additionalSpeed));
        fsm.AddState(new PlayerFSMStateJump(fsm, this, animator, jumpSpeed));
        fsm.AddState(new PlayerFSMStateFall(fsm, this, animator, groundChecker));
        fsm.AddState(new PlayerFSMStateInVehicle(fsm, driver, animator));
        fsm.SetState<PlayerFSMStateOnFoot>();

        driver.OnSwitchedHandler += cameraController.SwitchTarget;
    }

    private void Update()
    {
        fsm.Update();
    }

    private void FixedUpdate()
    {
        fsm.FixedUpdate();
    }

    protected override void OnGotHit()
    {
        base.OnGotHit();
        cameraController.enabled = false;
    }

    protected override void FinishDamageEffect()
    {
        base.FinishDamageEffect();
        cameraController.enabled = true;
    }

    protected override void OnDied()
    {
        base.OnDied();
        Destroy(playerStats);
        Destroy(driver);
        Destroy(cameraController);
        Destroy(interactionSystem);
        Destroy(this);
    }

    private void OnDestroy()
    {
        driver.OnSwitchedHandler -= cameraController.SwitchTarget;
    }
}
