using System;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class ThiefStealingSystem : MonoBehaviour
{
    public event Action<Interactable> OnCatchedInteractableHandler;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Interactable>(out var interactable))
        {
            OnCatchedInteractableHandler?.Invoke(interactable);
        }
    }
}
