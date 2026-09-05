using UnityEngine;

public abstract class Controller : MonoBehaviour
{
    [SerializeField] private int bodyMass;

    protected Animator animator;

    protected Rigidbody rb;

    protected Ragdoll ragdoll;

    protected GroundChecker groundChecker;

    protected DamageableStats stats;

    protected void Awake()
    {
        animator = GetComponent<Animator>();
        ragdoll = GetComponent<Ragdoll>();
        groundChecker = GetComponent<GroundChecker>();

        stats = GetComponent<DamageableStats>();
        stats.OnDiedHandler += OnDied;
        stats.OnGotHitHandler += OnGotHit;

        AddRigidBody();
    }

    protected void Start()
    {
        ragdoll.EnableRagdoll(false);
    }

    private void AddRigidBody()
    {
        rb = gameObject.AddComponent<Rigidbody>();
        rb.mass = bodyMass;
        rb.constraints = RigidbodyConstraints.FreezeRotation;
        rb.collisionDetectionMode = CollisionDetectionMode.Continuous;
    }

    private void DestroyRigidBody()
    {
        Destroy(rb);
    }

    public void EnableController()
    {
        AddRigidBody();
        //enabled = true;
    }

    public void DisableController()
    {
        DestroyRigidBody();
        //enabled = false;
    }

    protected virtual void OnGotHit()
    {
        ragdoll.EnableRagdoll(true);
        //rb.AddForce(-transform.forward * 40 * rb.mass, ForceMode.Impulse);
        enabled = false;
        Invoke(nameof(FinishDamageEffect), 3);
    }

    protected virtual void FinishDamageEffect()
    {
        ragdoll.EnableRagdoll(false);
        enabled = true;
    }

    protected virtual void OnDied()
    {
        animator.SetTrigger("Die");
        Destroy(ragdoll);
    }

    private void OnDestroy()
    {
        stats.OnDiedHandler -= OnDied;
        stats.OnGotHitHandler -= OnGotHit;
    }
}
