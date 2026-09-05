using System;
using UnityEngine;

public class Mission : MonoBehaviour
{
    private DeliveryPackage package;

    [SerializeField] private string missionName;

    [SerializeField] private string task;

    [SerializeField] private Transform destination;

    public bool isMissionTaken;

    private bool isMissionCompleted;

    public event Action OnMissionCompletedHandler;

    private void Awake()
    {
        package = GetComponent<DeliveryPackage>();
    }

    public void Complete()
    {
        isMissionCompleted = true;
        OnMissionCompletedHandler?.Invoke();
        Debug.Log("completed");
    }

    public string GetName()
    {
        return missionName;
    }

    public string GetTask()
    {
        return task;
    }

    public Transform GetDestination()
    {
        return destination;
    }

    public DeliveryPackage GetPackage()
    {
        return package;
    }

    public bool GetCompletedState()
    {
        return isMissionCompleted;
    }

}
