using UnityEngine;
using UnityEngine.Animations.Rigging;
using System;

public class GraspingHand : MonoBehaviour
{
    private TwoBoneIKConstraint handIK;
    [SerializeField] private Transform handTarget;
    [SerializeField] private Transform socket;
    [SerializeField] private float IKBlendSpeed;

    private float currentTargetIKWeight;

    protected Grabable currentItem;
    private bool needToInterpolateIKWeight;

    public Action<GameObject> OnNewItemHoldedHandler;

    private void Awake()
    {
        handIK = GetComponent<TwoBoneIKConstraint>();
    }

    public virtual void SetGrabable(Grabable grabable)
    {
        if (!currentItem)
        {
            currentItem = grabable;
            needToInterpolateIKWeight = true;
            currentTargetIKWeight = 1;
            GrabItem();
            OnNewItemHoldedHandler?.Invoke(currentItem.gameObject);
        }
    }

    public virtual void DismantleCurrentGrabable()
    {
        currentItem.transform.SetParent(null);
        currentItem = null;
        needToInterpolateIKWeight = true;
        currentTargetIKWeight = 0;
    }

    private void FixedUpdate()
    {
        if (needToInterpolateIKWeight)
        {
            if (Mathf.Abs(handIK.weight - currentTargetIKWeight) > 0.03f)
            {
                ReachTargetIKWeight();
            }
            else
            {
                needToInterpolateIKWeight = false;
            }
        }
    }

    private void ReachTargetIKWeight()
    {
        handIK.weight = Mathf.Lerp(handIK.weight, currentTargetIKWeight, IKBlendSpeed * Time.fixedDeltaTime);
    }

    private void GrabItem()
    {
        currentItem.transform.position = socket.position;
        currentItem.transform.rotation = socket.rotation;
        currentItem.transform.SetParent(socket);
    }

    public void ThrowCurrentItem()
    {
        currentItem.GetThrown(10);
        DismantleCurrentGrabable();
    }

    public Grabable GetCurrentItem()
    {
        return currentItem;
    }
}
