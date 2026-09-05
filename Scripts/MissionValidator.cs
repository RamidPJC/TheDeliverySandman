using System;
using System.Collections;
using UnityEngine;

public class MissionValidator : MonoBehaviour
{
    [SerializeField] private Mission targetMission;
    [SerializeField] private PackageSpotting spot;

    private bool validated;

    private void OnTriggerEnter(Collider other)
    {
        if (validated) return;

        if (!other.TryGetComponent<Mission>(out var mission))
            return;

        if (mission != targetMission)
        {
            spot.DeclinePackage();
            return;
        }
            
        validated = true;

        var package = mission.GetPackage();
        spot.AcceptPackage(package);
        Destroy(spot);

        mission.Complete();
        enabled = false;
    }
}

