using UnityEngine;

public class Ragdoll : MonoBehaviour
{
    [SerializeField] private Transform ragdollBody;

    private Rigidbody hipsRb;

    private Rigidbody[] rigidBodies;

    private Animator animator;

    private bool isRagdollEnabled;

    void Awake()
    {
        hipsRb = ragdollBody.GetComponent<Rigidbody>();
        rigidBodies = ragdollBody.GetComponentsInChildren<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        if (isRagdollEnabled)
        {
            hipsRb.position = transform.position;
        }
    }

    public void EnableRagdoll(bool enabled)
    {
        isRagdollEnabled = enabled;

        foreach (Rigidbody rb in rigidBodies)
        {
            Collider collider = rb.transform.GetComponent<Collider>();
            if (!enabled)
            {
                rb.isKinematic = true;
                if (collider)
                    collider.enabled = false;
            }
            else
            {
                rb.isKinematic = false;
                if (collider)
                    collider.enabled = true;
            }
        }

        if (!enabled)
            animator.enabled = true;
        else
            animator.enabled = false;
    }
}
