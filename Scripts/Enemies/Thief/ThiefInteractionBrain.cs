using System;
using UnityEngine;

public class ThiefInteractionBrain : InteractionBrain
{
    [SerializeField] private ThiefStealingSystem stealingSystem;
    public event Action OnCatchedStealableHandler;

    protected new void Awake()
    {
        base.Awake();
        stealingSystem.OnCatchedInteractableHandler += OnCatchInteractable;
    }

    protected override void OnCatchInteractable(Interactable interactable)
    {
        base.OnCatchInteractable(interactable);
        if (currentGrabable)
            OnCatchedStealableHandler?.Invoke();
    }
}
