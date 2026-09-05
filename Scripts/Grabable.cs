using System;
using UnityEngine;

public class Grabable : Interactable
{
    protected Rigidbody rb;
    [SerializeField] private Vector3 centerOfMass;

    private bool isGrabbed;

    protected void Awake()
    {
        rb = GetComponent<Rigidbody>();

        rb.centerOfMass = centerOfMass;
    }

    public override void Interact()
    {
        GetGrabbed();
    }

    public virtual void GetGrabbed()
    {
        rb.isKinematic = true;
        isGrabbed = true;
    }

    public virtual void GetThrown(float force)
    {
        GetReleased();
        rb.AddForce(transform.forward * force, ForceMode.VelocityChange);
    }

    public virtual void GetReleased()
    {
        rb.isKinematic = false;
        isGrabbed = false;
    }

    private void OnDisable()
    {
        GetReleased();
    }
}
