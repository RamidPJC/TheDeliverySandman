using UnityEngine;

public class CarController : MonoBehaviour
{
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private float checkGroundRadius;

    [SerializeField] private Vector3 centerOfMass;

    [SerializeField] private Transform frontLeftWheel;
    [SerializeField] private Transform frontRightWheel;
    [SerializeField] private Transform backLeftWheel;
    [SerializeField] private Transform backRightWheel;

    [SerializeField] private float maxSpeed;
    [SerializeField] private float timeToReachMaxSpeed;
    [SerializeField] private float maxSteerAngle;
    [SerializeField] private float maxTurnStrength;

    [SerializeField] private float timeToReachZero;

    private float currentSteerAngle;

    private Rigidbody rb;

    private float currentForceDir;

    private float currentTForEngineForce;

    private float currentStartingFromSpeed;
    private float currentStartingFromVertical;
    private Vector3 currentStartingFromVelocityVector;

    private float currentTForBrake;
    private float currentSpeed;
    private bool isBraking;

    private bool isTurning;
    private bool isGainingSpeed;

    private float hor;
    private float ver;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.centerOfMass = centerOfMass;
    }

    private void Update()
    {
        hor = Input.GetAxis("Horizontal");
        currentSteerAngle = hor * maxSteerAngle;
        //ApplySteerAngle();

        ver = Input.GetAxis("Vertical");

        currentForceDir = Mathf.Sign(Vector3.Dot(rb.linearVelocity.normalized, transform.forward));

        if (Input.GetButtonDown("Vertical"))
        {
            currentStartingFromVertical = Input.GetAxisRaw("Vertical");

            currentStartingFromSpeed = rb.linearVelocity.FromXZ().magnitude * currentForceDir;
            currentStartingFromVelocityVector = rb.linearVelocity;
            currentTForEngineForce = 0;
        }

        if (Input.GetButtonDown("Brake") && CheckGround())
        {
            isBraking = true;
            currentSpeed = rb.linearVelocity.FromXZ().magnitude;
            currentTForEngineForce = 0;
            currentTForBrake = 0;
        }

        if (Input.GetButtonUp("Brake") && CheckGround())
        {
            isBraking = false;
        }
    }

    private void FixedUpdate()
    {
        if (hor != 0 && CheckGround())
        {
            ApplyCarTorque();
        }

        if (ver != 0 && CheckGround() && !isBraking)
        {
            ApplyEngineForce();
        }

        if (isBraking && CheckGround())
        {
            ApplyBrake();
        }
    }

    private void ApplySteerAngle()
    {
        frontLeftWheel.localRotation = Quaternion.Euler(frontLeftWheel.localEulerAngles.x, currentSteerAngle, frontLeftWheel.localEulerAngles.z);

        frontRightWheel.localRotation = Quaternion.Euler(frontRightWheel.localEulerAngles.x, currentSteerAngle, frontRightWheel.localEulerAngles.z);
    }

    private void ApplyCarTorque()
    {
        //float speed = rb.linearVelocity.FromXZ().magnitude;
        //if (speed < 0.5f) return;

        //float steer01 = currentSteerAngle / maxSteerAngle;

        //float t = Mathf.Clamp01(speed / 10f);

        //float turnStrength = Mathf.Lerp(0.5f, maxTurnStrength, t);
        //float torque = steer01 * turnStrength;

        //rb.AddTorque(transform.up * torque, ForceMode.Acceleration);

        float speed = rb.linearVelocity.FromXZ().magnitude;
        if (speed < 0.5f) return;

        float steer01 = currentSteerAngle / maxSteerAngle;

        // ослабляем поворот при сильном боковом скольжении
        float lateral = Mathf.Abs(Vector3.Dot(rb.linearVelocity, transform.right));
        float lateralFactor = Mathf.Clamp01(1f - lateral / 10f);

        float t = Mathf.Clamp01(speed / 10f);
        float turnStrength = Mathf.Lerp(0.5f, maxTurnStrength, t);

        float torque = steer01 * turnStrength * lateralFactor;

        rb.AddTorque(transform.up * torque, ForceMode.Acceleration);
    }

    private void ApplyEngineForce()
    {
        currentTForEngineForce += Time.fixedDeltaTime / timeToReachMaxSpeed;
        currentTForEngineForce = Mathf.Clamp01(currentTForEngineForce);

        float x = Mathf.Lerp(currentStartingFromVelocityVector.x, maxSpeed * currentStartingFromVertical * transform.forward.x, currentTForEngineForce);
        float y = Mathf.Lerp(currentStartingFromVelocityVector.y, maxSpeed * currentStartingFromVertical * transform.forward.y, currentTForEngineForce);
        float z = Mathf.Lerp(currentStartingFromVelocityVector.z, maxSpeed * currentStartingFromVertical * transform.forward.z, currentTForEngineForce);

        Vector3 velocity = new Vector3(x, y, z);

        rb.linearVelocity = velocity;
    }

    private void ApplyBrake()
    {
        currentTForBrake += Time.fixedDeltaTime / timeToReachZero;
        currentTForBrake = Mathf.Clamp01(currentTForBrake);

        float speed = Mathf.Lerp(currentSpeed, 0, currentTForBrake);
        Vector3 forward = rb.linearVelocity.normalized;
        rb.linearVelocity = forward * speed;
    }

    private void OnDisable()
    {
        isBraking = false;
    }

    private bool CheckGround()
    {
        return Physics.CheckSphere(frontLeftWheel.position, checkGroundRadius, groundMask);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawSphere(frontLeftWheel.position, checkGroundRadius);
    }
}
