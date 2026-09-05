using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class VehicleController : MonoBehaviour
{
    private Rigidbody rb;

    [SerializeField] private WheelCollider frontRightWheel;
    [SerializeField] private WheelCollider frontLeftWheel;
    [SerializeField] private WheelCollider backRightWheel;
    [SerializeField] private WheelCollider backLeftWheel;

    [SerializeField] private float maxAcceleration;
    [SerializeField] private float maxBrakeTorque;
    [SerializeField] private float maxSteerAngle;

    private float currentAcceleration = 0;
    private float currentBrakeTorque = 0;
    private float currentSteerAngle;

    private float currentVertAxis = 0;
    private float currentHorAxis = 0;
    private float currentBrakeAxis = 0;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        Vector3 com = rb.centerOfMass;
        com.y = -0.4f;
        rb.centerOfMass = com;
    }

    private void Update()
    {
        currentVertAxis = Input.GetAxis("Vertical");
        currentHorAxis = Input.GetAxis("Horizontal");
        currentBrakeAxis = Input.GetAxis("Brake");

        if (Input.GetButton("Brake"))
        {
            ApplyBrakeTorque();
        }
    }

    private void FixedUpdate()
    {
        ApplyMotorForce();

        //ApplyBrakeTorque();

        ApplyWheelsSteer();
    }

    private void ApplyMotorForce()
    {
        currentAcceleration = maxAcceleration * currentVertAxis;

        frontRightWheel.motorTorque = currentAcceleration;
        frontLeftWheel.motorTorque = currentAcceleration;
        backRightWheel.motorTorque = currentAcceleration;
        backLeftWheel.motorTorque = currentAcceleration;
    }

    private void ApplyBrakeTorque()
    {
        currentBrakeTorque = maxBrakeTorque * currentBrakeAxis;

        frontRightWheel.brakeTorque = maxBrakeTorque;
        frontLeftWheel.brakeTorque = maxBrakeTorque;
        backRightWheel.brakeTorque = maxBrakeTorque;
        backLeftWheel.brakeTorque = maxBrakeTorque;
    }

    private void ApplyWheelsSteer()
    {
        currentSteerAngle = maxSteerAngle * currentHorAxis;

        frontRightWheel.steerAngle = currentSteerAngle;
        frontLeftWheel.steerAngle = currentSteerAngle;
    }
}
