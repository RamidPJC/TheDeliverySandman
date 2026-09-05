using UnityEngine;

public class ImpulseExp : MonoBehaviour
{
    private Rigidbody rb;
    [SerializeField] private Transform target;
    [SerializeField] private float force;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        Vector3 dir = target.position - transform.position;
        Debug.Log(dir.magnitude);
        dir.y = 7f;
        rb.AddForce(dir.normalized * force * rb.mass, ForceMode.Impulse);
    }
}
