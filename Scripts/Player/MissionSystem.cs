using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MissionSystem : MonoBehaviour
{
    private Mission currentMission;

    public event Action<Transform> OnGetMissionAccepedHandler;

    public event Action<Mission> OnNewMissionPickedUpHandler;

    public event Action OnMissionCompleted;

    public void OnNewMissionPickedUp(Mission newMission)
    {
        if (currentMission != newMission && !newMission.isMissionTaken)
        {
            if (currentMission)
            {
                currentMission.isMissionTaken = false;
                currentMission.OnMissionCompletedHandler -= OnCurrentMissionCompleted;
            }
                
            currentMission = newMission;
            currentMission.OnMissionCompletedHandler += OnCurrentMissionCompleted;
            currentMission.isMissionTaken = true;

            OnNewMissionPickedUpHandler?.Invoke(currentMission);

            OnGetMissionAccepedHandler?.Invoke(currentMission.GetDestination());
        }
    }

    public void TryRegisterGrabable(Grabable grabable)
    {
        if (grabable is DeliveryPackage package)
        {
            Debug.Log("reg");
            package.OnPickedUpHandler += OnNewMissionPickedUp;
            OnNewMissionPickedUp(package.GetMission());
        }
    }

    public void UnregisterGrabable(Grabable grabable)
    {
        if (grabable is DeliveryPackage package)
        {
            Debug.Log("unreg");
            package.OnPickedUpHandler -= OnNewMissionPickedUp;
        }
    }

    private void OnCurrentMissionCompleted()
    {
        OnGetMissionAccepedHandler?.Invoke(null);
        OnMissionCompleted?.Invoke();
        currentMission = null;
    }

    public void DeclineCurrentMission()
    {
        OnGetMissionAccepedHandler?.Invoke(null);
        currentMission.isMissionTaken = false;
        currentMission.OnMissionCompletedHandler -= OnCurrentMissionCompleted;
        currentMission = null;
    }
}
