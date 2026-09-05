using UnityEngine;

public class GroundChecker : MonoBehaviour
{
    public Transform checkPoint;
    [SerializeField] private float radius = 0.2f;
    [SerializeField] private LayerMask groundMask;

    public bool CheckGround()
    {
        return Physics.CheckSphere(checkPoint.position, radius, groundMask, QueryTriggerInteraction.Ignore);
    }
}
