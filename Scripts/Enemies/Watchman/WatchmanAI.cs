using UnityEngine;

public class WatchmanAI : EnemyAI
{
    [SerializeField] private Transform[] patrolPoints;
    private bool isPatroling = true;
    private Transform currentPatrolPoint;

    [SerializeField] private float patrolSpeed;

    [SerializeField] private Gun gun;
    [SerializeField] private Transform raycastOrigin;

    protected new void Awake()
    {
        base.Awake();
        CalculateNewPatrolPoint();
    }

    protected new void Start()
    {
        base.Start();
        gun.GetGrabbed();
    }

    private void FixedUpdate()
    {
        Vector3 dir;

        if (isPatroling)
        {
            dir = currentPatrolPoint.position - transform.position;

            if (groundChecker.CheckGround())
                MoveToPoint(dir);

            if (Vector3.Distance(transform.position, currentPatrolPoint.position) <= 3)
            {
                CalculateNewPatrolPoint();
            }
        }
        else
        {
            dir = currentTarget.position - transform.position;
            gun.Shoot(rb.linearVelocity, GetShootRay(dir));
        }

        dir.y = 0;
        LookAtDir(dir);

        SetBlendTreeMotion();
    }

    protected override void OnMovingObjectDetected(bool entered, MovingObject target)
    {
        base.OnMovingObjectDetected(entered, target);
        if (entered)
            isPatroling = false;
        else
            isPatroling = true;
    }

    private void CalculateNewPatrolPoint()
    {
        currentPatrolPoint = patrolPoints[Random.Range(0, patrolPoints.Length)];
    }

    private void MoveToPoint(Vector3 dir)
    {
        rb.linearVelocity = dir.normalized * patrolSpeed;
    }

    private void LookAtDir(Vector3 dir)
    {
        Quaternion targetRot = Quaternion.LookRotation(dir);
        if (!isPatroling)
            targetRot = Quaternion.Euler(targetRot.eulerAngles.x, targetRot.eulerAngles.y + Random.Range(-60, 60), targetRot.eulerAngles.z);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, 10 * Time.fixedDeltaTime);
    }

    private RaycastHit GetShootRay(Vector3 dir)
    {
        RaycastHit hit;
        if (Physics.Raycast(raycastOrigin.position, dir, out hit, 1000))
            return hit;

        RaycastHit fakeHit = new RaycastHit();
        fakeHit.point = raycastOrigin.position + dir.normalized * 1000f;
        return fakeHit;
    }

    private void SetBlendTreeMotion()
    {
        Vector2 velocity = rb.linearVelocity.FromXZ();
        float axis = velocity.magnitude > 1 ? velocity.normalized.magnitude : velocity.magnitude;
        animator.SetFloat("Vertical", axis);
    }
}
