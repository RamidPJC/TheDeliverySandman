using System;
using UnityEngine;

public class DeliveryPackage : Grabable
{
    private Collider col;

    private bool isLifted;

    public event Action<Mission> OnPickedUpHandler;

    private Mission mission;

    private bool isAttached;

    protected new void Awake()
    {
        base.Awake();
        mission = GetComponent<Mission>();
        col = GetComponent<Collider>();
    }

    public override void GetGrabbed()
    {
        base.GetGrabbed();

        isLifted = true;
    }

    public override void GetReleased()
    {
        base.GetReleased();

        isLifted = false;
    }

    public bool GetLiftingState()
    {
        return isLifted;
    }

    public void GetAttached()
    {
        isAttached = true;
        rb.isKinematic = true;
        col.isTrigger = true;
    }

    public void GetDetached()
    {
        isAttached = false;
        col.isTrigger = false;
    }

    public void BecomeUninteractable()
    {
        col.enabled = false;
    }

    public Mission GetMission()
    {
        return mission;
    }

    public bool GetAttachedState()
    {
        return isAttached;
    }
}
