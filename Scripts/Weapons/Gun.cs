using UnityEngine;

public class Gun : Weapon, IUsable
{
    [SerializeField] private float gunpowderForce;
    [SerializeField] private float fireRate;
    private float nextShootTime;

    [SerializeField] private Transform muzzle;
    [SerializeField] private GameObject shootEffect;
    [SerializeField] private Ammo ammo;

    [SerializeField] private Animator animator;

    [Header("Spread")]
    [SerializeField] private float maxSpreadAngle = 2f;
    [SerializeField] private float speedForMaxSpread = 5f;

    private bool isShooting;

    public override void GetGrabbed()
    {
        base.GetGrabbed();
        animator.enabled = true;
    }

    public override void GetReleased()
    {
        base.GetReleased();
        animator.enabled = false;
    }

    public void Use(IUserContext context)
    {
        if (Time.time < nextShootTime)
            return;

        Shoot(context.GetVelocity(), context.GetRay());
        nextShootTime = Time.time + 1f / fireRate;
    }

    public void StopUse()
    {
        shootEffect.SetActive(false);
    }

    public void Shoot(Vector3 userVelocity, RaycastHit hit)
    {
        Bullet bullet = ammo.TryGetBullet();
        if (!bullet)
        {
            if (isShooting)
            {
                isShooting = false;
                StopUse();
                animator.SetTrigger("Reload");
            }
            return;
        }

        isShooting = true;
        shootEffect.SetActive(true);

        bullet.transform.position = muzzle.position;
        bullet.transform.rotation = muzzle.rotation;
        bullet.gameObject.SetActive(true);

        Vector3 baseDirection;
        if (hit.collider)
            baseDirection = (hit.point - muzzle.position).normalized;
        else
            baseDirection = muzzle.forward;

        Vector3 finalDirection = ApplySpread(baseDirection, userVelocity.magnitude);

        bullet.rb.linearVelocity = Vector3.zero;
        bullet.rb.AddForce(finalDirection * gunpowderForce, ForceMode.VelocityChange);

        animator.SetTrigger("Shoot");
    }

    private Vector3 ApplySpread(Vector3 direction, float speed)
    {
        float spread01 = Mathf.Clamp01(speed / speedForMaxSpread);
        float currentSpread = maxSpreadAngle * spread01;

        float yaw = Random.Range(-currentSpread, currentSpread);
        float pitch = Random.Range(-currentSpread, currentSpread);

        Quaternion spreadRotation = Quaternion.Euler(pitch, yaw, 0f);
        return spreadRotation * direction;
    }
}
