using UnityEngine;

public enum PositionControlMode
{
    Follow,
    OrbitalFollow
}

public enum RotationControlMode
{
    RotateWithFollowTarget,
    RotationComposer
}

public class CameraSocketSettings : MonoBehaviour
{
    public PositionControlMode socketPositionControlMode;
    public RotationControlMode socketRotationControlMode;

    public Vector3 positionFollowOffset;
    public Vector3 damping;
}
