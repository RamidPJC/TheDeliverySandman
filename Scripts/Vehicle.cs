using System;
using System.Collections;
using UnityEngine;

public class Vehicle : MonoBehaviour
{
    private RCCP_CarController vehicleController;

    [SerializeField] private Transform settle;
    [SerializeField] private Transform exitPoint;

    private Driver currentDriver;

    private bool isDriverInside;

    private void Start()
    {
        vehicleController = GetComponent<RCCP_CarController>();
    }

    public Transform GetSettle()
    {
        return settle;
    }

    public Transform GetExitPoint()
    {
        return exitPoint;
    }

    public void TryToUnblock(Driver driver)
    {
        if (currentDriver && driver != currentDriver)
        {
            currentDriver.Toggle(this);
            Block();
        }

        currentDriver = driver;
        vehicleController.enabled = true;
    }

    public void Block()
    {
        currentDriver = null;
        vehicleController.enabled = false;
    }
}
