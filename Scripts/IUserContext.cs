using UnityEngine;

public interface IUserContext
{
    public Vector3 GetVelocity();

    public RaycastHit GetRay();
}
