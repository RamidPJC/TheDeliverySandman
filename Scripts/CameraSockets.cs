using System.Collections.Generic;
using UnityEngine;

public class CameraSockets : MonoBehaviour
{
    [SerializeField] private List<Transform> cameraSockets;

    public List<Transform> GetCameraSockets()
    {
        return cameraSockets;
    }
}
