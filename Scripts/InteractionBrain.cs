using UnityEngine;

public abstract class InteractionBrain : MonoBehaviour
{
    [SerializeField] protected GraspingHand graspingHand;
    [SerializeField] protected ActionsInputSystem ownerInputSystem;

    protected Grabable currentGrabable;

    protected void Awake()
    {
        ownerInputSystem.OnThrowActionEnteredHandler += OnThrowEntered;
    }

    protected virtual void OnCatchInteractable(Interactable interactable)
    {
        interactable.Interact();

        currentGrabable = interactable.GetComponent<Grabable>();

        if (currentGrabable)
            graspingHand.SetGrabable(currentGrabable);
    }

    protected void OnThrowEntered()
    {
        if (graspingHand.GetCurrentItem())
        {
            graspingHand.ThrowCurrentItem();
        }
    }
}
