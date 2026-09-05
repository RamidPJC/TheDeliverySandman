using UnityEngine;

public abstract class User : MonoBehaviour, IUserContext
{
    private IUsable currentUsable;
    private Rigidbody rb;
    [SerializeField] private ActionsInputSystem inputSystem;
    [SerializeField] private GraspingHand graspingHand;

    private bool isUsing;

    protected void Awake()
    {
        graspingHand.OnNewItemHoldedHandler += TryRegisterUsable;
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void TryRegisterUsable(GameObject obj)
    {
        if (obj.TryGetComponent<IUsable>(out var usable))
        {
            currentUsable = usable;
        }
    }

    private void Update()
    {
        if (currentUsable != null)
        {
            if (inputSystem.isUseActionHolded)
            {
                if (!isUsing)
                    isUsing = true;
                currentUsable.Use(this);
            }
            else
            {
                if (isUsing)
                {
                    isUsing = false;
                    currentUsable.StopUse();
                }
                    
            }
        }
    }

    public Vector3 GetVelocity()
    {
        return rb.linearVelocity;
    }

    public abstract RaycastHit GetRay();
}
