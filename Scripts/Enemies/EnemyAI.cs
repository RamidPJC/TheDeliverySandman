using UnityEngine;

public class EnemyAI : Controller
{
    [SerializeField] protected EnemyArea detectionArea;

    public Transform currentTarget;

    protected new void Start()
    {
        base.Start();
        detectionArea.OnMovingObjectDetectedHandler += OnMovingObjectDetected;
    }

    protected virtual void OnMovingObjectDetected(bool entered, MovingObject target)
    {
        if (entered)
        {
            currentTarget = target.transform;
        }
    }
}
