using UnityEngine;

public class MovingObject : MonoBehaviour
{
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public float GetMovingVelocity()
    {
        return rb.linearVelocity.magnitude;
    }
}
