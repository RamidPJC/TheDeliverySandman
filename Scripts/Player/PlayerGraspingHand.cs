using UnityEngine;
using UnityEngine.Animations.Rigging;

public class PlayerGraspingHand : GraspingHand
{
    [SerializeField] private MultiRotationConstraint shoulderIK;

    public override void SetGrabable(Grabable grabable)
    {
        base.SetGrabable(grabable);
        if (currentItem)
            shoulderIK.weight = 1;
    }

    public override void DismantleCurrentGrabable()
    {
        base.DismantleCurrentGrabable();
        shoulderIK.weight = 0;
    }
}
