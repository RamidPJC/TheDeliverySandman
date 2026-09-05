using System;
using System.Collections.Generic;
using UnityEngine;

public class InteractionHand : MonoBehaviour
{
    private Rigidbody rb;

    private Animator animator;

    private bool isGrabing;

    private Interactable currentInteractable;

    public Action<Interactable> OnCatchInteractableHandler;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetButtonDown("Select"))
        {
            Grab();
            OnCatchInteractableHandler?.Invoke(currentInteractable);
        }
    }

    private void Grab()
    {
        animator.SetTrigger("Grab");
    }

    public bool GetGrabingState()
    {
        return isGrabing;
    }

    public void SetNewInteractable(Interactable interactable)
    {
        currentInteractable = interactable;
    }
}
