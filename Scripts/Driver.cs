using System;
using System.Collections;
using TMPro.Examples;
using Unity.Cinemachine;
using UnityEngine;

public class Driver : MonoBehaviour
{
    Controller controller;

    public event Action<Transform, CameraTargetType> OnSwitchedHandler;

    public event Action OnEnteredVehicle;
    public event Action OnExitedHandler;

    private bool isInVehicle;
    private Vehicle currentVehicle;

    private void Start()
    {
        controller = GetComponent<Controller>();
    }

    public void Toggle(Vehicle vehicle)
    {
        if (!isInVehicle)
        {
            EnterVehicle(vehicle.GetSettle());
            vehicle.TryToUnblock(this);
            isInVehicle = true;
            currentVehicle = vehicle;
        }
        else
        {
            ExitVehicle();
            vehicle.Block();
            isInVehicle = false;
            currentVehicle = null;
        }
    }

    private void EnterVehicle(Transform seat)
    {
        OnEnteredVehicle?.Invoke();

        transform.SetParent(seat);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;

        OnSwitchedHandler?.Invoke(seat, CameraTargetType.Vehicle);
        OnEnteredVehicle?.Invoke();
        controller.DisableController();
    }

    private void ExitVehicle()
    {
        

        transform.SetParent(null);
        transform.position = currentVehicle.GetExitPoint().position;

        OnSwitchedHandler?.Invoke(transform, CameraTargetType.Player);
        controller.EnableController();
        OnExitedHandler?.Invoke();
    }
}
