using System;
using UnityEngine;

public class EnemyArea : MonoBehaviour
{
    public event Action<bool, MovingObject> OnMovingObjectDetectedHandler;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<MovingObject>(out var movingObject))
        {
            Debug.Log(movingObject.gameObject.name + " in");
            OnMovingObjectDetectedHandler?.Invoke(true, movingObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<MovingObject>(out var movingObject))
        {
            Debug.Log(movingObject.gameObject.name + " out");
            OnMovingObjectDetectedHandler?.Invoke(false, movingObject);
        }
    }
}
