using System.Linq;
using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Only for example purposes
/// </summary>
public class MinimapAgentRegistrator : MonoBehaviour
{
    [SerializeField] private GameObject _player;
    [SerializeField] private Minimap _minimap;

    private void Start()
    {
        _minimap.RegisterPlayer(_player);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<StaticNPCAgent>(out var agent))
        {
            _minimap.Register(agent);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<StaticNPCAgent>(out var agent))
        {
            _minimap.Unregister(agent);
        }
    }
}