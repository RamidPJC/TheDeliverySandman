using System;
using System.Security.Cryptography;
using UnityEngine;

public enum SpotMode
{
    DeliverMode,
    ValidateMode
}

public class PackageSpotting : MonoBehaviour
{
    [SerializeField] private SpotMode mode;
    public Outline outline;

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.TryGetComponent<DeliveryPackage>(out var package))
        {
            if (!outline.enabled)
            {
                outline.enabled = true;
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.transform.TryGetComponent<DeliveryPackage>(out var package))
        {
            switch (mode)
            {
                case SpotMode.DeliverMode:
                    if (!package.GetLiftingState())
                    {
                        AttachPackage(package);
                    }
                    else if (package.GetLiftingState() && package.GetAttachedState())
                    {
                        DetachPackage(package);
                    }
                    break;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.TryGetComponent<DeliveryPackage>(out var package))
        {
            if (outline.enabled)
            {
                outline.enabled = false;
            }
        }
    }

    #region ValidatorModeOnly
    public void AcceptPackage(DeliveryPackage package)
    {
        outline.OutlineColor = Color.green;
        package.BecomeUninteractable();
        AttachPackage(package);
        enabled = false;
    }

    public void DeclinePackage()
    {
        outline.OutlineColor = Color.red;
    }
    #endregion

    private void AttachPackage(DeliveryPackage package)
    {
        package.GetAttached();
        package.transform.position = transform.position;
        package.transform.rotation = transform.rotation;
        package.transform.SetParent(transform);

        outline.enabled = false;
    }

    private void DetachPackage(DeliveryPackage package)
    {
        package.GetDetached();
    }
}
